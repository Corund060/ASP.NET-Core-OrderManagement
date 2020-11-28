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
    /// User controller for User Registering and Loging
    /// </summary>
    public class UserController : Controller
    {
        public Database db = new Database();
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Remove user information from Session
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("currentUser");
            return View("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Verify user crediantials on Login
        /// </summary>
        /// <param name="loginName">User entered Login Name</param>
        /// <param name="password">User entered Password</param>
        /// <returns></returns>
        public IActionResult Verify(string loginName, string password)
        {
            if (db.VerifyUser(loginName, password))
            {
                HttpContext.Session.SetString("currentUser", loginName);
                return View("Index");
            }
            ViewData["errorMsg"] = "Wrong Login name and/or password";
            return View("Login");
        }

        /// <summary>
        /// Cecking user inputs and Registering new User
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="password2"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="adress"></param>
        /// <returns></returns>
        public IActionResult VerifyRegister(string loginName, string password, string password2, string firstName, string lastName, string adress)
        {
            if (loginName == "" || password == "" || password2 == "" || loginName == null || password == null || password2 == null)
            {
                ViewData["errorMsg"] = "Missing information.";
                return View("Register");
            }
            if (db.UserExists(loginName))
            {
                ViewData["errorMsg"] = "This name is taken.";
                return View("Register");
            }
            if (password != password2)
            {
                ViewData["errorMsg"] = "Passwords dont match.";
                return View("Register");
            }

            db.AddUser(loginName, password, firstName, lastName, adress);
            return View("Registered");
        }
    }
}
