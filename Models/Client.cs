using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace OrdersSystem
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Name")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Surname")]
        public string ClientSurname { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Email")]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Family card number")]
        public int FamilyCardId { get; set; }

        public virtual FamilyCard FamilyCard { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
