using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Courier
    {
        public Courier()
        {
            Orders = new HashSet<Order>();
        }

        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string CourierSurname { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
