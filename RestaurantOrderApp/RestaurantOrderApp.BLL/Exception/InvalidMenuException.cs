using System;
using System.Collections.Generic;
using System.Text;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.BLL
{
    public class InvalidMenuException : Exception
    {
        public InvalidMenuException(TimeOfDay timeOfDay) : base($"Invalid Menu {timeOfDay.ToString()}")
        {
        }
    }
}
