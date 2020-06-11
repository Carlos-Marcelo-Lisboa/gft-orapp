using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestaurantOrderApp.Interface;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.BLL
{
    public class Order : IOrder
    {
        private IMenuRepository menuRepository;
        private Queue<string> queuedItems;
        private TimeOfDay timeOfDay;

        public Order(IMenuRepository MenuRepository)
        {
            menuRepository = MenuRepository;
            queuedItems = new Queue<string>();
        }

        public Queue<string> ReadInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new InvalidEmptyInputException();

            var items = new Queue<string>(input.Split(','));

            if (items.Count == 0)
                throw new InvalidEmptyInputException();


            return items;
        }

        public Menu GetMenuFromRepository(TimeOfDay timeOfDay)
        {
            Menu menu = menuRepository.GetMenu(timeOfDay);
            if (menu == null)
                throw new InvalidMenuException(timeOfDay);

            return menu;

        }

        public TimeOfDay DequeueTimeOfDay(Queue<string> input)
        {
            string time = input.Dequeue().ToLower();
            if (Enum.IsDefined(typeof(TimeOfDay), time))
                return (TimeOfDay)Enum.Parse(typeof(TimeOfDay), time);
            else
                throw new InvalidTimeOfDayException(time);
        }

        public Dictionary<int, int> GetOrderItemsQuantity(Queue<string> queuedItems)
        {
            Dictionary<int, int> OrderItemsQuantity = new Dictionary<int, int>();
            foreach (string item in queuedItems)
            {
                int itemNumber;
                if (!int.TryParse(item, out itemNumber))
                    throw new InvalidOrderItemException(item);

                if (!OrderItemsQuantity.ContainsKey(itemNumber))
                    OrderItemsQuantity.Add(itemNumber, 0);

                OrderItemsQuantity[itemNumber]++;

            }
            return OrderItemsQuantity;
        }

        public string FormatOutput(string Name, int quantity)
        {
            if (quantity < 1)
                return String.Empty;

            if (quantity > 1)
                return $"{Name}(x{quantity})";

            return Name;
                
        }

        private string ProcessOutput(Menu menu, Dictionary<int,int>OrderItemsQuantity)
        {
            var stroutput = new StringBuilder();
            foreach (var item in OrderItemsQuantity.OrderBy(p => p.Key))
            {
                var dish = menu.Dishes.Where(p => (int)p.Type == item.Key).FirstOrDefault();

                if (dish == null)
                {
                    stroutput.Append("error");
                    break;
                }

                if(item.Value > dish.MaxQuantity || item.Value < dish.MinQuantity)
                {
                    stroutput.Append("error");
                    break;
                }

                stroutput.Append(FormatOutput(menu.Dishes.Where(p => (int)p.Type == item.Key).Select(p => p.Name).FirstOrDefault(), item.Value)).Append(", "); ;

            }
            return stroutput.ToString().TrimEnd().TrimEnd(new char[] { ',' }); ; 
        }

        public string ProcessOrder(string input)
        {
            queuedItems = ReadInput(input);

            timeOfDay = DequeueTimeOfDay(queuedItems);

            if (queuedItems.Count == 0)
                throw new InvalidEmptyInputException();

            return ProcessOutput(GetMenuFromRepository(timeOfDay), GetOrderItemsQuantity(queuedItems));                       

        }

    }
}
