using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class Town
    {
        public Town()
        {
            Streets = new HashSet<Street>();
        }

        public int TownId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string TownName { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Postal code")]
        public int TownPostCode { get; set; }
        

        public virtual ICollection<Street> Streets { get; set; }
    }
}
