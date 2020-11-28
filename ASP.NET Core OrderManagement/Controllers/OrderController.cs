using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Order controler for Updating and Filtering of Orders
    /// </summary>
    public class OrderController : Controller
    {
        public Database db = new Database();

        /// <summary>
        /// Showing all orders in database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["filter"] = "All";
            return View(db.GetOrders());
        }

        /// <summary>
        /// UPdating status of Order in database
        /// </summary>
        /// <param name="id">Id of Order to update</param>
        /// <param name="status">New status of Updated Order</param>
        /// <returns></returns>
        public IActionResult ChangeStatus(int id, string status)
        {
            db.UpdateOrderStatus(id, status);
            return View();
        }

        /// <summary>
        /// Filter Orders according to users choice
        /// </summary>
        /// <param name="statusFilter">Filter from dropbox</param>
        /// <returns></returns>
        public IActionResult FilterStatus(string statusFilter)
        {
            ViewData["filter"] = statusFilter;
            return View("Index", db.GetOrders(statusFilter));
        }
    }
}
