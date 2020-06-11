using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp.BLL
{
    public abstract class InvalidInputException : Exception
    {
        public InvalidInputException(string innerMessage) : base("The input is not valid.", (string.IsNullOrEmpty(innerMessage) ? null : new Exception(innerMessage)))
        {
        }
    }
}
