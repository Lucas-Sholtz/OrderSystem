using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class FamilyCard
    {
        public FamilyCard()
        {
            Clients = new HashSet<Client>();
        }

        public int FamilyCardId { get; set; }
        public double FamilyCardBalance { get; set; }
        public double FamilyCardBonusPercent { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
