using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Item controller for Creating Reading Updating Deleting of Items
    /// </summary>
    public class ItemController : Controller
    {
        public Database db = new Database();


        /// <summary>
        /// Get list of Items from database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(db.GetItems().ToList());
        }

        /// <summary>
        /// Add new Item to list of Items in database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public IActionResult AddItem(string name, double price, double discount)
        {
            db.AddItem(name, price, discount);
            return View("Index", db.GetItems().ToList());
        }

        /// <summary>
        /// Delete Item from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteItem(int id)
        {
            db.DeleteItem(id);
            return View("Index", db.GetItems().ToList());
        }

        /// <summary>
        /// Update Item property depending on what was edited
        /// </summary>
        /// <param name="id">Edited field information with Item Id</param>
        /// <param name="value">New value of updated field</param>
        /// <returns></returns>
        public IActionResult UpdateItem(string id, string value)
        {
            string[] field = id.Split(':');
            switch (field[0])
            {
                case "name":
                    db.UpdateItemName(Convert.ToInt32(field[1]), value);
                    break;
                case "price":
                    db.UpdateItemPrice(Convert.ToInt32(field[1]), Convert.ToDouble(value));
                    break;
                case "discount":
                    db.UpdateItemDiscount(Convert.ToInt32(field[1]), Convert.ToDouble(value));
                    break;
            }
            return View("Index", db.GetItems().ToList());
        }
    }
}
