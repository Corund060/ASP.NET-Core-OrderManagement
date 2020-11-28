using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;

namespace OrderManagement.Models
{
    public class Database
    {
        public DatabaseContext DBContext { get; set; }

        public Database()
        {
            DBContext = new DatabaseContext();
        }

        /// <summary>
        /// Get List of all Items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            return DBContext.Items.ToList();
        }

        /// <summary>
        /// Get Item by database Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Item GetItemById(int id)
        {
            return DBContext.Items.Where(itm => itm.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Add new Item to database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="discount"></param>
        public void AddItem(string name, double price, double discount)
        {
            DBContext.Items.Add(new Item { 
                Name=name,
                Price=price,
                Discount=discount
            });
            DBContext.SaveChanges();
        }        

        /// <summary>
        /// Delete Item from Database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteItem(int id)
        {
            DBContext.Items.Remove(DBContext.Items.Where(itm => itm.Id == id).FirstOrDefault());
            DBContext.SaveChanges();
        }

        public void UpdateItemName(int id, string name)
        {
            DBContext.Items.Where(itm => itm.Id == id).FirstOrDefault().Name = name;
            DBContext.SaveChanges();
        }

        public void UpdateItemPrice(int id, double price)
        {
            DBContext.Items.Where(itm => itm.Id == id).FirstOrDefault().Price = price;
            DBContext.SaveChanges();
        }

        public void UpdateItemDiscount(int id, double discount)
        {
            DBContext.Items.Where(itm => itm.Id == id).FirstOrDefault().Discount = discount;
            DBContext.SaveChanges();
        }

        public int AddBasket(Basket newBasket)
        {
            DBContext.Baskets.Add(newBasket);           
            DBContext.SaveChanges();
            return DBContext.Baskets.Max(item => item.Id);
        }

        public void ClearBasketById(int id)
        {           
            DBContext.Baskets.Remove(DBContext.Baskets.Where(bask => bask.Id == id).FirstOrDefault());
            DBContext.SaveChanges();            
        }

        public Basket GetBasketById(int id)
        {
            return DBContext.Baskets.Include(bask=>bask.BasketItems).Where(bask => bask.Id == id).FirstOrDefault();
        }

        public void AddOrder(Order newOrder)
        {
            DBContext.Orders.Add(newOrder);
            DBContext.SaveChanges();
        }

        public void UpdateOrderStatus(int id, string status)
        {
            DBContext.Orders.Where(ord => ord.Id == id).FirstOrDefault().OrderStatus = status;
            DBContext.SaveChanges();
        }

        public List<Order> GetOrders()
        {            
            return DBContext.Orders.Include(inc => inc.OrderCustomer).ToList();
        }

        public List<Order> GetOrders(string status)
        {
            if (status=="All")
            {
                return DBContext.Orders.Include(inc => inc.OrderCustomer).ToList();
            }
            return DBContext.Orders.Include(inc => inc.OrderCustomer).Where(ord=>ord.OrderStatus==status).ToList();
        }

        public User GetUser(string name)
        {
            return DBContext.Users.Where(usr => usr.LoginName == name).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return DBContext.Users.ToList();
        }

        public void AddUser(string loginName, string userPass, string firstName, string lastName, string adress)
        {
            DBContext.Users.Add(new User { 
                LoginName=loginName,
                Password=userPass,
                FirstName=firstName,
                LastName=lastName,
                Adress=adress
            });
            DBContext.SaveChanges();
        }

        public bool VerifyUser(string loginName, string password)
        {
            return DBContext.Users.Where(usr => usr.LoginName == loginName && usr.Password == password).Any();
        }

        public bool UserExists(string userName)
        {
            return DBContext.Users.Where(usr => usr.LoginName == userName).Any();
        }
    }
}
