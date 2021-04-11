using project2.Entities;
using project2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project2.Controllers
{

    public class CitiesController : Controller
    {
        // GET: Cities
        public ActionResult Index()
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

            CitiesRepository repo = new CitiesRepository();
            ViewData["cities"] = repo.GetAll();

            return View();
        }

        [HttpGet]
        public ActionResult Add()
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

            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {


            City c = new City();
            c.Name = name;

            CitiesRepository repo = new CitiesRepository();
            repo.Insert(c);

            return RedirectToAction("Index", "Cities");
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

            CitiesRepository repo = new CitiesRepository();
            repo.Delete(id);

            return RedirectToAction("Index", "Cities");
        }

    }
}
