using System;
using System.Collections.Generic;
using RestaurantOrderApp.BLL;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;
using RestaurantOrderApp.Repositoriy;

namespace RestaurantOrderApp.SmokeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order(new MenuRepository(new DAL.RestaurantOrderAppContext()));
                  


            Console.WriteLine(order.ProcessOrder("morning, 1, 2, 3"));
            Console.WriteLine(order.ProcessOrder("morning, 2, 1, 3"));
            Console.WriteLine(order.ProcessOrder("morning, 2, 1, 3, 4"));
            Console.WriteLine(order.ProcessOrder("morning, 1, 2, 3, 3, 3"));

            Console.WriteLine(order.ProcessOrder("night, 1, 2, 3, 4"));
            Console.WriteLine(order.ProcessOrder("night, 1, 2, 2, 4"));
            Console.WriteLine(order.ProcessOrder("night, 1, 2, 3, 5"));
            Console.WriteLine(order.ProcessOrder("night, 1, 1, 2, 3, 5"));


            Console.ReadKey();

        }
    }
}
