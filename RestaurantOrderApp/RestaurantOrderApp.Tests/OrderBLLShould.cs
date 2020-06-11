using System;
using System.Collections.Generic;
using RestaurantOrderApp.BLL;
using RestaurantOrderApp.DAL;
using RestaurantOrderApp.Interface;
using RestaurantOrderApp.Repositoriy;
using Xunit;
using FluentAssertions;
using System.Linq;
using RestaurantOrderApp.Model.Enum;
using Moq;
using RestaurantOrderApp.Model;
using FluentAssertions.Execution;

namespace RestaurantOrderApp.Tests
{

    /// <summary>
    /// Test the business logic layer
    /// Using fluent assertions in order to improve readbility, error messages and use of scope
    /// Using Moq framework to avoid testing the Repository itself
    /// </summary>
    public class OrderBLLShould
    {
        IOrder sut;
        Mock<IMenuRepository> MenuRepositaryMock;

        public OrderBLLShould()
        {
            //Create object to mock te Repository interface
            MenuRepositaryMock = new Mock<IMenuRepository>();
            MenuRepositaryMock.Setup(p => p.GetMenu(It.IsAny<TimeOfDay>())).Returns((TimeOfDay timeOfDay) => {
                var dishes = new List<Dish>();
                switch(timeOfDay)
                {
                    case TimeOfDay.morning:
                        dishes.Add(new Dish() { Time = TimeOfDay.morning, Type = DishType.entree, Name = "eggs", MaxQuantity = 1, MinQuantity = 1 });
                        dishes.Add(new Dish() { Time = TimeOfDay.morning, Type = DishType.side, Name = "toast", MaxQuantity = 1, MinQuantity = 1 });
                        dishes.Add(new Dish() { Time = TimeOfDay.morning, Type = DishType.drink, Name = "coffee", MaxQuantity = 100, MinQuantity = 1 });
                        break;
                    case TimeOfDay.night:
                        dishes.Add(new Dish() { Time = TimeOfDay.night, Type = DishType.entree, Name = "steak", MaxQuantity = 1, MinQuantity = 1 });
                        dishes.Add(new Dish() { Time = TimeOfDay.night, Type = DishType.side, Name = "potato", MaxQuantity = 100, MinQuantity = 1 });
                        dishes.Add(new Dish() { Time = TimeOfDay.night, Type = DishType.drink, Name = "wine", MaxQuantity = 1, MinQuantity = 1 });
                        dishes.Add(new Dish() { Time = TimeOfDay.night, Type = DishType.dessert, Name = "cake", MaxQuantity = 1, MinQuantity = 1 });
                        break;
                }                             
                return new Menu(timeOfDay, dishes);
            });
            sut = new Order(MenuRepositaryMock.Object);        }

        [Fact]
        public void ReadAValidInput()
        {
            using (new AssertionScope())
            {
                sut.ReadInput("morning, 1, 2, 3, 3, 3").Should().NotBeEmpty().And.HaveCount(6).And.StartWith("morning");
            }
        }

        [Fact]
        public void ReadAnInValidInput()
        {
            sut.Invoking(p => p.ReadInput("")).Should().Throw<InvalidEmptyInputException>();
        }

        [Fact]
        public void DequeueTimeOfDay()
        {
            var items = new Queue<string>();
            items.Enqueue("morning");
            items.Enqueue("1");
            items.Enqueue("2");
            items.Enqueue("3");
            sut.DequeueTimeOfDay(items).Should().Be(TimeOfDay.morning);
        }


        [Fact]
        public void DequeueAnInvalidTimeOfDay()
        {
            var items = new Queue<string>();
            items.Enqueue("afternoon");
            items.Enqueue("1");
            items.Enqueue("2");
            items.Enqueue("3");
            sut.Invoking(p => p.DequeueTimeOfDay(items)).Should().Throw<InvalidTimeOfDayException>();
        }


        [Fact]
        public void GetOrderItemsQuantity()
        {
            var items = new Queue<string>();
            items.Enqueue("1");
            items.Enqueue("2");
            items.Enqueue("3");
            items.Enqueue("3");
            items.Enqueue("3");
            var count = new Dictionary<int, int>();
            count.Add(1, 1);
            count.Add(2, 1);
            count.Add(3, 3);
            sut.GetOrderItemsQuantity(items).Should().Equal(count);
        }

        [Fact]
        public void GetAnInvalidOrderItemsQuantity()
        {
            var items = new Queue<string>();
            items.Enqueue("1");
            items.Enqueue("2");
            items.Enqueue("lemon");
            sut.Invoking(p => p.GetOrderItemsQuantity(items)).Should().Throw<InvalidOrderItemException>();
        }

        [Fact]
        public void FormatOutput()
        {
            using (new AssertionScope())
            {
                sut.FormatOutput("egg", 3).Should().Be("egg(x3)");
                sut.FormatOutput("egg", 1).Should().Be("egg");
                sut.FormatOutput("egg", 0).Should().Be("");
            }
        }


        [Fact]
        public void GetMenuFromRepository()
        {
            using (new AssertionScope())
            {
                var menu = sut.GetMenuFromRepository(TimeOfDay.morning);
                menu.Should().NotBeNull();
                menu.TimeOfDay.Should().Be(TimeOfDay.morning);
            }
        }

        [Fact]
        public void ProcessOrder()
        {
            using (new AssertionScope())
            {
                sut.ProcessOrder("morning, 2, 1, 3").Should().Be("eggs, toast, coffee");
                sut.ProcessOrder("morning, 1, 2, 3, 4").Should().Be("eggs, toast, coffee, error");
                sut.ProcessOrder("morning, 1, 2, 3, 3, 3").Should().Be("eggs, toast, coffee(x3)");
                sut.ProcessOrder("night, 1, 2, 2, 4").Should().Be("steak, potato(x2), cake");
                sut.ProcessOrder("night, 1, 2, 3, 5").Should().Be("steak, potato, wine, error");
            }
        }

    }
}
