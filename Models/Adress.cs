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
        [Range(1, 999, ErrorMessage = "Only numbers from 1 to 999 allowed")]
        public int AddressStreetNumber { get; set; }
        [Display(Name = "Notes")]
        public string AddressNotes { get; set; }

        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Street")]
        public int StreetId { get; set; }
        
        public string AddressString 
        {
            get
            {
                string s = "";
                s += Street.Town.TownName + ", ";
                s += Street.StreetName + ", ";
                s += AddressStreetNumber;
                if (AddressNotes != null)
                {
                    s += ", ";
                    s += AddressNotes;
                }
                return s;
            }
        }
        public virtual Street Street { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
