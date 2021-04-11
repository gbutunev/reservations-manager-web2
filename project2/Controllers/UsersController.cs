using project2.Entities;
using project2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project2.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            User loggedUser = (User)Session["loggedUser"];
            if (!loggedUser.isAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            UsersRepository repo = new UsersRepository();
            ViewData["users"] = repo.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //ADMIN CHECK
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            User loggedUser = (User)Session["loggedUser"];
            if (!loggedUser.isAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            UsersRepository repo = new UsersRepository();
            User current = repo.GetById(id);

            ViewData["id"] = current.Id;
            ViewData["firstName"] = current.FirstName;
            ViewData["lastName"] = current.LastName;
            ViewData["username"] = current.Username;
            ViewData["admin"] = current.isAdmin.ToString();
            ViewData["password"] = current.Password.ToString();

            return View();

        }

        [HttpPost]
        public ActionResult Edit(int id, string firstName, string lastName, string username, Boolean admin, string password)
        {
            //CitiesRepository crepo = new CitiesRepository();

            User u = new User
            {
                Id = id,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                isAdmin = admin,
                Password = password

            };

            UsersRepository repo = new UsersRepository();
            repo.Update(u);


            return RedirectToAction("Index", "Users");
        }

        public ActionResult Delete(int id)
        {
            //ADMIN CHECK
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            User loggedUser = (User)Session["loggedUser"];
            if (!loggedUser.isAdmin)
            {
                return RedirectToAction("Index", "Home");
            }

            UsersRepository repo = new UsersRepository();
            repo.Delete(id);

            return RedirectToAction("Index", "Users");
        }
    }
}