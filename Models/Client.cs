using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientEmail { get; set; }
        public int FamilyCardId { get; set; }

        public virtual FamilyCard FamilyCard { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
