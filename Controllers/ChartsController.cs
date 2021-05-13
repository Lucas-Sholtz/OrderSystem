using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrdersSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly OrderSystemDatabaseContext _context;
        public ChartsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("Couriers")]
        public JsonResult Couriers()
        {
            var couriers = _context.Couriers.Include(c => c.Orders).ToList();

            List<object> list = new List<object>();
            list.Add(new [] { "Сourier", "Order count" });
            foreach(var c in couriers)
            {
                list.Add(new object[] { c.CourierName, c.Orders.Count() });
            }

            return new JsonResult(list);
        }

        [HttpGet("Shops")]
        public JsonResult Shops()
        {
            var shops = _context.Shops.Include(c => c.Products).ToList();

            List<object> list = new List<object>();
            list.Add(new[] { "Shop", "Products count" });
            foreach (var c in shops)
            {
                list.Add(new object[] { c.ShopName, c.Products.Count() });
            }

            return new JsonResult(list);
        }
    }
}
