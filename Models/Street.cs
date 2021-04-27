using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class Street
    {
        public Street()
        {
            Adresses = new HashSet<Adress>();
        }

        public int StreetId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }
        public virtual ICollection<Adress> Adresses { get; set; }
    }
}
