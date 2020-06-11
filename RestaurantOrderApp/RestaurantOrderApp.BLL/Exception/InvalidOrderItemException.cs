using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp.BLL
{
    public class InvalidOrderItemException : InvalidInputException
    {
        public InvalidOrderItemException(string orderItem) : base($"{orderItem} is an invalid order option")
        {
        }
    }
}
