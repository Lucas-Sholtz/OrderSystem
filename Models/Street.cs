using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Street
    {
        public Street()
        {
            Adresses = new HashSet<Adress>();
        }

        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public int CityId { get; set; }

        public virtual Сity City { get; set; }
        public virtual ICollection<Adress> Adresses { get; set; }
    }
}
