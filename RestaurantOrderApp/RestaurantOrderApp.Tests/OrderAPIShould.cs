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
using RestaurantOrderApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantOrderApp.Tests
{

    /// <summary>
    /// Test the business logic layer
    /// Using fluent assertions in order to improve readbility, error messages and use of scope
    /// Using Moq framework to avoid testing the Repository itself
    /// </summary>
    public class OrderAPIShould
    {
        OrderController sut;
        Mock<IMenuRepository> MenuRepositaryMock;
        OrderController orderController;

        public OrderAPIShould()
        {
            //Create object to mock te Repository interface
            MenuRepositaryMock = new Mock<IMenuRepository>();
            MenuRepositaryMock.Setup(p => p.GetMenu(It.IsAny<TimeOfDay>())).Returns((TimeOfDay timeOfDay) => {
                var dishes = new List<Dish>();
                switch (timeOfDay)
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
            sut = new OrderController(null, new Order(MenuRepositaryMock.Object));
        }

        [Fact]
        public void ProcessOrder()
        {
            using (new AssertionScope())
            {
                sut.ProcessOrder("morning, 2, 1, 3").As<OkObjectResult>().StatusCode.Should().Be(200);
                sut.ProcessOrder("morning, 2, 1, 3").As<OkObjectResult>().StatusCode.Should().Be(200);
                sut.ProcessOrder("morning, 1, 2, 3, 4").As<OkObjectResult>().StatusCode.Should().Be(200);
                sut.ProcessOrder("morning, 1, 2, 3, 3, 3").As<OkObjectResult>().StatusCode.Should().Be(200);
                sut.ProcessOrder("night, 1, 2, 2, 4").As<OkObjectResult>().StatusCode.Should().Be(200);
                sut.ProcessOrder("night, 1, 2, 3, 5").As<OkObjectResult>().Value.Should().Be("steak, potato, wine, error");
            }
        }

    }
}
