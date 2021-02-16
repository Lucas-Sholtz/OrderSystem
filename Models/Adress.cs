using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Adress
    {
        public Adress()
        {
            Orders = new HashSet<Order>();
            Shops = new HashSet<Shop>();
        }

        public int AddressId { get; set; }
        public int AddressStrretNumber { get; set; }
        public string AddressNotes { get; set; }
        public int StreetId { get; set; }

        public virtual Street Street { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
