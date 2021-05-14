using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersSystem.Models
{
    public class Query
    {
        public string QueryId { get; set; }
        public string CourierName { get; set; }
        public string ShopName { get; set; }
        public decimal AveragePrice { get; set; }
        public string TownName { get; set; }
        public List<string> ShopNames { get; set; }
        public string FirstShopName { get; set; }
        public string SecondShopName { get; set; }
        public List<string> SameProductNames { get; set; }
        public int OrderId { get; set; }
        public decimal OrderPrice { get; set; }
        [Required(ErrorMessage = "Required field")]
        [Range(0,int.MaxValue)]
        public int ProductPrice { get; set; }
    }
}
