using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Product
    {
        public Product()
        {
            ProductOrderPairs = new HashSet<ProductOrderPair>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductRemainingQuantity { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<ProductOrderPair> ProductOrderPairs { get; set; }
    }
}
