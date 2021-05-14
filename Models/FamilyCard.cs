using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class FamilyCard
    {
        public FamilyCard()
        {
            Clients = new HashSet<Client>();
        }

        public int FamilyCardId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Balance")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double FamilyCardBalance { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Bonus percent")]
        [Range(0.0, 100.0, ErrorMessage = "Only value between 0 and 100")]
        public double FamilyCardBonusPercent { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
