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
        public double FamilyCardBalance { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Bonus percent")]
        public double FamilyCardBonusPercent { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
