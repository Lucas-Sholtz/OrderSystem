using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Shop
    {
        public Shop()
        {
            Couriers = new HashSet<Courier>();
            Products = new HashSet<Product>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int AddressId { get; set; }

        public virtual Adress Address { get; set; }
        public virtual ICollection<Courier> Couriers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
