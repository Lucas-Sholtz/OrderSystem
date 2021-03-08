using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrdersSystem
{
    public partial class ProductOrderPair
    {
        public int PairId { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Product number")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Order number")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
