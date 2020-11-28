using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class BasketItem
    {
        [Key]
        public int Id { get; set; }

        public int BasketItemId { get; set; }

        public string BasketItemName { get; set; }

        public double BasketItemPrice { get; set; }

        public double BasketItemDiscount { get; set; }

        public int BasketItemQuantity { get; set; }
    }
}
