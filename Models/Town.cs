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
        [Range(1, 99999, ErrorMessage = "Only 5 digit values allowed")]
        public int TownPostCode { get; set; }
        

        public virtual ICollection<Street> Streets { get; set; }
    }
}
