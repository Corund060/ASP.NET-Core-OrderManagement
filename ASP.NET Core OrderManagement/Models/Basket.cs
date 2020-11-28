using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class Basket
    {        
        [Key]
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public double BasketDiscount { get; set; }     

        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }

        /// <summary>
        /// Returns Total value of Basket items with Discount
        /// </summary>
        /// <returns></returns>
        public double TotalAmount()
        {
            double totalAmount = 0;
            foreach (var item in BasketItems)
            {
                totalAmount += item.BasketItemPrice * item.BasketItemQuantity;
            }
            return totalAmount-totalAmount*(BasketDiscount/100);
        }

        /// <summary>
        /// Calculate all Items in Basket
        /// </summary>
        /// <returns></returns>
        public int ItemCount()
        {
            int itemCount = 0;
            foreach (var item in BasketItems)
            {
                itemCount += item.BasketItemQuantity;
            }
            return itemCount;
        }
    }
}
