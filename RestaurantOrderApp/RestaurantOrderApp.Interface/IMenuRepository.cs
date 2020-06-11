using System;
using System.Collections.Generic;
using System.Text;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;

namespace RestaurantOrderApp.Interface
{
    public interface IMenuRepository
    {
        Menu GetMenu(TimeOfDay timeOfDay);
    }
}
