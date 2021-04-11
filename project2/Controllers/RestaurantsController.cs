using project2.Entities;
using project2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project2.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: Restaurants
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

            //create repo
            RestaurantsRepository repo = new RestaurantsRepository();
            ViewData["restaurants"] = repo.GetAll();

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
        public ActionResult Add(string name, int seats, string location)
        {
            //needed repos
            CitiesRepository cityRepo = new CitiesRepository();
            RestaurantsRepository restRepo = new RestaurantsRepository();

            //add restaurant
            Restaurant r = new Restaurant()
            {
                Name = name,
                LocationId = cityRepo.GetByName(location).Id,
                Seats = seats
            };
            restRepo.Insert(r);

            return RedirectToAction("Index", "Restaurants");
        }

        public ActionResult Delete(int id)
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

            RestaurantsRepository repo = new RestaurantsRepository();
            repo.Delete(id);

            return RedirectToAction("Index", "Restaurants");
        }
    }
}