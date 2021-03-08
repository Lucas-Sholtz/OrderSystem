using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrdersSystem
{
    public partial class Order
    {
        public Order()
        {
            ProductOrderPairs = new HashSet<ProductOrderPair>();
        }

        public int OrderId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Delivered")]
        public bool OrderStatus { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Client number")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Address number")]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "This field can't be empty!")]
        [Display(Name = "Courier number")]
        public int CourierId { get; set; }

        public virtual Adress Address { get; set; }
        public virtual Client Client { get; set; }
        public virtual Courier Courier { get; set; }
        public virtual ICollection<ProductOrderPair> ProductOrderPairs { get; set; }
    }
}
