using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class Courier
    {
        public Courier()
        {
            Orders = new HashSet<Order>();
        }

        public int CourierId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string CourierName { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Surname")]
        public string CourierSurname { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Shop")]
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
