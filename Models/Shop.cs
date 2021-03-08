using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrdersSystem
{
    public partial class Shop
    {
        public Shop()
        {
            Couriers = new HashSet<Courier>();
            Products = new HashSet<Product>();
        }

        public int ShopId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Address number")]
        public int AddressId { get; set; }

        public virtual Adress Address { get; set; }
        public virtual ICollection<Courier> Couriers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
