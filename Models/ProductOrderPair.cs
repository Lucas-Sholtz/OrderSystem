using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class ProductOrderPair
    {
        public int PairId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
