using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class Adress
    {
        public Adress()
        {
            Orders = new HashSet<Order>();
            Shops = new HashSet<Shop>();
        }

        public int AddressId { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "House number")]
        public int AddressStreetNumber { get; set; }
        [Display(Name = "Notes")]
        public string AddressNotes { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Street number")]
        public int StreetId { get; set; }

        public virtual Street Street { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
