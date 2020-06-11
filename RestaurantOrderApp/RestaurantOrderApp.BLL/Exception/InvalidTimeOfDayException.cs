using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp.BLL
{
    public class InvalidTimeOfDayException : InvalidInputException
    {
        public InvalidTimeOfDayException(string timeOfDay) : base($"{timeOfDay} is an invalid time of the day")
        {
        }
    }
}
