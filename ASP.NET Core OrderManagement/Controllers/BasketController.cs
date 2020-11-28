using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models;
using Microsoft.AspNetCore.Http;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Basket controller for Creating Reading Updating Deleting of basket Items
    /// </summary>
    public class BasketController : Controller
    {
        public Database db = new Database();

        /// <summary>
        /// Returns a numeric value of Basket Id for current session
        /// </summary>
        /// <returns></returns>
        public int BasketId()
        {
            return Convert.ToInt32(HttpContext.Session.GetString("SessionBasketId"));
        }

        public IActionResult Index()
        {
            ViewBag.Users = db.GetUsers();
            if (BasketId() != 0 && db.GetBasketById(BasketId()).BasketItems.Count() > 0)
            {
                return View(db.GetBasketById(BasketId()));
            }
            return View("Empty");
        }

        public IActionResult Empty()
        {
            return View();
        }

        /// <summary>
        /// Add item to Basket if Basket already created, create Basket and add item if not.
        /// </summary>
        /// <param name="id"> Item id from the list of database Items</param>
        /// <returns></returns>
        public IActionResult AddItem(int id)
        {
            if (HttpContext.Session.GetString("SessionBasketId") == null)
            {
                HttpContext.Session.SetString("SessionBasketId", db.AddBasket(new Basket { }).ToString());
            }
            if (db.GetBasketById(BasketId()).BasketItems.Where(bask => bask.BasketItemId == id).Any())
            {
                db.GetBasketById(BasketId()).BasketItems.Where(ordItm => ordItm.BasketItemId == id).FirstOrDefault().BasketItemQuantity++;
            }
            else
            {
                Item itm = db.GetItemById(id);
                db.GetBasketById(BasketId()).BasketItems.Add(new BasketItem
                {
                    BasketItemId = itm.Id,
                    BasketItemName = itm.Name,
                    BasketItemPrice = itm.Price,
                    BasketItemDiscount = itm.Discount,
                    BasketItemQuantity = 1
                });
            }
            db.DBContext.SaveChanges();

            // Saving Basket id as a Session string
            HttpContext.Session.SetString("ItemsInBasket", db.GetBasketById(BasketId()).ItemCount().ToString());

            ViewBag.Users = db.GetUsers();
            return View("Index", db.GetBasketById(BasketId()));
        }

        /// <summary>
        /// Called when user lowers quantity of item in the Basket
        /// </summary>
        /// <param name="id">Id of database Item</param>
        /// <returns></returns>
        public IActionResult SubstractItem(int id)
        {
            Item itm = db.GetItemById(id);

            if (db.GetBasketById(BasketId()).BasketItems.Where(bask => bask.BasketItemId == id).FirstOrDefault().BasketItemQuantity >= 2)
            {
                db.GetBasketById(BasketId()).BasketItems.Where(bask => bask.BasketItemId == id).FirstOrDefault().BasketItemQuantity--;
                HttpContext.Session.SetString("ItemsInBasket", db.GetBasketById(BasketId()).ItemCount().ToString());
                db.DBContext.SaveChanges();
            }
            ViewBag.Users = db.GetUsers();
            return View("Index", db.GetBasketById(BasketId()));
        }

        /// <summary>
        /// Called when user lower quantity of Item in the Basket to 0
        /// </summary>
        /// <param name="id">Id of database Item</param>
        /// <returns></returns>
        public IActionResult DeleteItem(int id)
        {
            db.GetBasketById(BasketId()).BasketItems.Remove(db.GetBasketById(BasketId()).BasketItems.Where(bask => bask.BasketItemId == id).FirstOrDefault());
            HttpContext.Session.SetString("ItemsInBasket", db.GetBasketById(BasketId()).ItemCount().ToString());
            ViewBag.Users = db.GetUsers();
            db.DBContext.SaveChanges();
            return View("Index", db.GetBasketById(BasketId()));
        }

        /// <summary>
        /// Remove Basket from Session
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearBasket()
        {
            HttpContext.Session.Remove("ItemsInBasket");
            HttpContext.Session.Remove("SessionBasketId");
            return View("Empty");
        }

        /// <summary>
        /// Transfer Basket to Order with Customer data. Clear Basket from Session.
        /// </summary>
        /// <param name="isNew">True if new User is created</param>
        /// <param name="loginName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="adress"></param>
        /// <param name="userPass"></param>
        /// <param name="orderDiscount"></param>
        /// <returns></returns>
        public IActionResult SaveBasket(bool isNew, string loginName, string firstName, string lastName, string adress, string userPass, string orderDiscount)
        {
            db.GetBasketById(BasketId()).BasketDiscount = Convert.ToDouble(orderDiscount);

            if (isNew)
            {
                db.AddUser(loginName, userPass, firstName, lastName, adress);
            }
            db.AddOrder(new Order
            {
                OrderDate = DateTime.Now,
                OrderCustomer = db.GetUser(loginName),
                OrderBasket = db.GetBasketById(BasketId()),
                OrderStatus = "Ordered",
                OrderProvider = new Provider(),
                OrderTotalAmount = db.GetBasketById(BasketId()).TotalAmount(),
            });
            ClearBasket();
            return View("OrderSummary");
        }
    }
}
