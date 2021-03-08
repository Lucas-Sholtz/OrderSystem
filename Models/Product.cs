using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrdersSystem
{
    public partial class Product
    {
        public Product()
        {
            ProductOrderPairs = new HashSet<ProductOrderPair>();
        }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Price")]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Remaining")]
        public int ProductRemainingQuantity { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Shop number")]
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<ProductOrderPair> ProductOrderPairs { get; set; }
    }
}
