using System;
using System.Collections.Generic;

#nullable disable

namespace OrderSystem
{
    public partial class Сity
    {
        public Сity()
        {
            Streets = new HashSet<Street>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CityPostCode { get; set; }

        public virtual ICollection<Street> Streets { get; set; }
    }
}
