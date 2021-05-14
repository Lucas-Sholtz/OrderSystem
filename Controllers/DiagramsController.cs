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
    public class DiagramsController : ControllerBase
    {
        private readonly OrderSystemDatabaseContext _context;
        public DiagramsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var shops = _context.Shops.Include(c => c.Products).ToList();
            //var table = new List<Tuple<string, int>>();
            List<object> list = new List<object>();
            list.Add(new[] { "Shop", "Products count" });
            foreach (var c in shops)
            {
                list.Add(new object[] { c.ShopName, c.Products.Count() });
                //table.Add(new Tuple<string, int>(c.CourierName, c.Orders.Count));
            }

            return new JsonResult(list);
        }
    }
}
