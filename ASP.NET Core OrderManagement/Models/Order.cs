using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public User OrderCustomer { get; set; }        
        public Basket OrderBasket { get; set; }       
        public string OrderStatus { get; set; }
        public double OrderTotalAmount { get; set; }
        public Provider OrderProvider { get; set; }
    }
}
