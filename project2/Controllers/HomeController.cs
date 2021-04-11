using project2.Entities;
using project2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace project2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isValid = true;
            string regexPattern = @"[\s]";

            Regex regx = new Regex(regexPattern);

            if ((username == "" || regx.IsMatch(username)) && (password == "" || regx.IsMatch(password)))
            {
                ViewData["loginError"] = "Please enter your username and password";
                isValid = false;
            }
            else if (username == "" || regx.Equals(username))
            {
                ViewData["loginError"] = "Please enter your username";
                isValid = false;
            }
            else if (password == "" || regx.Equals(password))
            {
                ViewData["loginError"] = "Please enter your password";
                isValid = false;
            }

            ViewData["username"] = username;

            if (!isValid)
            {
                return View();
            }
            else
            {
                UsersRepository repo = new UsersRepository();
                User loggedUser = repo.GetFirstOrDefault(u => u.Username == username && u.Password == password);

                if (loggedUser == null)
                {
                    ViewData["loginError"] = "Invalid username or password";
                    return View();
                }
                else
                {
                    Session["loggedUser"] = loggedUser;
                    return RedirectToAction("Index", "Home");
                }
            }


        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string fname, string lname)
        {
            string regexPattern = @"[\s]";
            Regex regx = new Regex(regexPattern);
            if (username == "")
            {
                ViewData["wrongInput"] = "Username cannot be blank!";
                return View();
            }
            else if (regx.IsMatch(username))
            {
                ViewData["wrongInput"] = "Username cannot contain spaces!";
                return View();
            }
            else if (password == "")
            {
                ViewData["wrongInput"] = "Password cannot be empty!";
                return View();
            }
            else if (regx.IsMatch(password))
            {
                ViewData["wrongInput"] = "Password cannot contain spaces!";
                return View();
            }
            else
            {

                User u = new User();
                //CitiesRepository repoasd = new CitiesRepository();

                u.Username = username;
                u.Password = password;
                u.FirstName = fname;
                u.LastName = lname;
                //u.Location = repoasd.GetByName(location);
                u.isAdmin = false;

                UsersRepository repo = new UsersRepository();
                repo.Insert(u);

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout()
        {
            if (Session["loggedUser"] != null)
            {
                Session["loggedUser"] = null;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}