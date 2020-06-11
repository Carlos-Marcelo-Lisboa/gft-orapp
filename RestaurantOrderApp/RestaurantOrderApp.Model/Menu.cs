using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.Model
{
    public class Menu
    {
        public TimeOfDay TimeOfDay { get; set; }
        public List<Dish> Dishes { get; set; }

        public Menu(TimeOfDay timeOfDay, List<Dish> dishes)
        {
            if (dishes.Where(p => p.Time != timeOfDay).Any())
                throw new ArgumentOutOfRangeException($"Only {timeOfDay} menu is allowed");

            if (dishes.GroupBy(p => p.Type).Where(grp => grp.Count() > 1).Any())
                throw new ArgumentOutOfRangeException($"Only one type is allowed");

            TimeOfDay = timeOfDay;
            Dishes = dishes;
        }
    }
}
