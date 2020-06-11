using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class OrderController : ControllerBase
    {
       
        private readonly ILogger<OrderController> _logger;
        private readonly IOrder _order;


        public OrderController(ILogger<OrderController> logger, IOrder order)
        {
            _logger = logger;
            _order = order;
        }

        //[HttpGet]
        //public string Get()
        //{
        //    return _order.ProcessOrder("morning, 1, 2, 3");
        //}

        [HttpPost]
        [HttpOptions]
        public IActionResult ProcessOrder([FromBody] string options)
        {
            try
            {
                return Ok(_order.ProcessOrder(options.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
