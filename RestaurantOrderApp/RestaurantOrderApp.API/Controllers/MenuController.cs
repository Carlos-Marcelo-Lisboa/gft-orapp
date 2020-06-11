using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantOrderApp.Interface;
using RestaurantOrderApp.Model;
using RestaurantOrderApp.Model.Enum;
using RestaurantOrderApp.Repositoriy;

namespace RestaurantOrderApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
       
        private readonly ILogger<OrderController> _logger;
        private readonly IMenuRepository _menuRepository;


        public MenuController(ILogger<OrderController> logger, IMenuRepository menuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
        }

        [HttpGet]
        public Menu get(string timeOfday)
        {
            return _menuRepository.GetMenu(TimeOfDay.morning);
        }
    }
}
