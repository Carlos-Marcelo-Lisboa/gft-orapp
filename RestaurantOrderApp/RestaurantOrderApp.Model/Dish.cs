using System;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.Model
{
    public class Dish
    {
        public DishType Type { get; set; }
        public TimeOfDay Time { get; set; }
        public string Name { get; set; }
        public int MaxQuantity { get; set; }
        public int MinQuantity { get; set; }
    }
}
