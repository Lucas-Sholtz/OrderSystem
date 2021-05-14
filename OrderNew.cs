using System;
using System.Collections.Generic;

#nullable disable

namespace OrdersSystem
{
    public partial class OrderNew
    {
        public OrderNew()
        {
            ProductOrderPairs = new HashSet<ProductOrderPair>();
        }

        public int OrderId { get; set; }
        public bool OrderStatus { get; set; }
        public int ClientId { get; set; }
        public int AddressId { get; set; }
        public int CourierId { get; set; }

        public virtual Adress Address { get; set; }
        public virtual Client Client { get; set; }
        public virtual Courier Courier { get; set; }
        public virtual ICollection<ProductOrderPair> ProductOrderPairs { get; set; }
    }
}
