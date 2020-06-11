using System;
using System.Collections.Generic;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.Interface
{
    public interface IOrder
    {
        Queue<string> ReadInput(string input);
        TimeOfDay DequeueTimeOfDay(Queue<string> input);
        Dictionary<int, int> GetOrderItemsQuantity(Queue<string> queuedItems);
        string FormatOutput(string Name, int quantity);
        string ProcessOrder(string input);
        Menu GetMenuFromRepository(TimeOfDay timeOfDay);
    }
}
