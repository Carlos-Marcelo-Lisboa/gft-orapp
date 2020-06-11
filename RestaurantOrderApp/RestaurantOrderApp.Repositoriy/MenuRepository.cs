using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantOrderApp.DAL;
using RestaurantOrderApp.Interface;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.Repositoriy
{
    public class MenuRepository : Repository<OrderOption>, IMenuRepository
    {
        public MenuRepository(RestaurantOrderAppContext context) : base(context) { }

        public Menu GetMenu(TimeOfDay timeOfDay)
        {            
            var items = ctx.OrderOptions.Where(p => p.TimeOfDay == (int)timeOfDay).OrderBy(p => p.DishType);
            List<Dish> dishList = new List<Dish>();
            foreach (var item in items)
            {
                dishList.Add(new Dish() { Time = (TimeOfDay)item.TimeOfDay, Type = (DishType)item.DishType, Name = item.Name, MaxQuantity = item.MaxQuantity, MinQuantity = item.MinQuantity });
            }

            return new Menu(timeOfDay, dishList);
        }
    }
}
