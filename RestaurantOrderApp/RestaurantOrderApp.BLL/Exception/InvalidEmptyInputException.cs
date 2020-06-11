using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp.BLL
{
    public class InvalidEmptyInputException : InvalidInputException
    {
        public InvalidEmptyInputException() : base("The input string must not be empty")
        {
        }
    }
}
