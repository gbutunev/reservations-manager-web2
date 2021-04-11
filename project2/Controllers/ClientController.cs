using project2.Entities;
using project2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project2.Controllers
{
    public class ClientController : Controller
    {
        public ActionResult Restaurants()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            RestaurantsRepository repo = new RestaurantsRepository();
            ViewData["restaurants"] = repo.GetAll().ToList();

            return View();
        }

        [HttpGet]
        public ActionResult CreateReservation()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //can add restaurants repo later to make a dropdown menu 
            return View();
        }

        [HttpPost]
        public ActionResult CreateReservation(string restaurant, int seats, DateTime time, DateTime date)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ReservationsRepository rezRepo = new ReservationsRepository();
            RestaurantsRepository resRepo = new RestaurantsRepository();

            User loggedUser = (User)Session["loggedUser"];

            DateTime resTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

            Reservation res = new Reservation()
            {
                Date = resTime, //////////////////////////////EDIT NECESSARY
                Seats = seats,
                RestaurantId = resRepo.GetByName(restaurant).Id,
                UserId = loggedUser.Id
            };

            rezRepo.Insert(res);


            return RedirectToAction("MyReservations", "Client");
        }

        public ActionResult MyReservations()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            User loggedUser = (User)Session["loggedUser"];
            ReservationsRepository rezRepo = new ReservationsRepository();

            ViewData["myReservations"] = rezRepo.GetAll(i => i.UserId == loggedUser.Id).ToList();

            return View();
        }

        public ActionResult DeleteReservation(int id)
        {

            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ReservationsRepository repo = new ReservationsRepository();
            repo.Delete(id);

            User loggedUser = (User)Session["loggedUser"];
            if (loggedUser.isAdmin)
            {
                return RedirectToAction("AllReservations", "Client");
            }
            else return RedirectToAction("MyReservations", "Client");
        }

        [HttpGet]
        public ActionResult EditReservation(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            RestaurantsRepository restRepo = new RestaurantsRepository();
            ReservationsRepository repo = new ReservationsRepository();
            Reservation currRes = repo.GetById(id);

            ViewData["restaurant"] = restRepo.GetById(currRes.RestaurantId).Name;
            ViewData["date"] = currRes.Date;
            ViewData["restaurantId"] = currRes.RestaurantId;
            ViewData["seats"] = currRes.Seats;
            ViewData["id"] = currRes.Id;

            return View();
            //ReservationsRepository rezRepo = new ReservationsRepository();
            //RestaurantsRepository resRepo = new RestaurantsRepository();

            //User loggedUser = (User)Session["loggedUser"];

            //Reservation res = new Reservation()
            //{
            //    Date = time,
            //    Seats = seats,
            //    RestaurantId = resRepo.GetByName(restaurant).Id,
            //    UserId = loggedUser.Id
            //};

            //rezRepo.Insert(res);
        }

        [HttpPost]
        public ActionResult EditReservation(int id, int restaurantId, int seats, DateTime date)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User loggedUser = (User)Session["loggedUser"];

            ReservationsRepository repo = new ReservationsRepository();

            Reservation res = new Reservation()
            {
                Id = id,
                RestaurantId = restaurantId,
                Date = date,
                Seats = seats,
                UserId = loggedUser.Id,
                Owner = loggedUser
            };

            repo.Update(res);
            return RedirectToAction("MyReservations", "Client");
        }

        public ActionResult AllReservations()
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

            ReservationsRepository repo = new ReservationsRepository();
            ViewData["reservations"] = repo.GetAllReservations().ToList();

            return View();
        }
    }
}