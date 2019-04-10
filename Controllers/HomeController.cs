using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using RentaCar.Models;
using System.Globalization;

namespace RentaCar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ShowAllCars()
        {

            Reservation GetCars = new Reservation();
            List<Cars> AvailableCars = GetCars.GetAvailablePackage();
            ViewBag.AvailableCars = AvailableCars;


            return View();
        }

        public ActionResult CreateReservation(string Id)
        {
            Reservation GetCarsId = new Reservation();
            List<Cars> CarForReservation = GetCarsId.GetCarById(Id);
            ViewBag.CarForReservation = CarForReservation;

            return View();
        }

        public ActionResult Conformation(FormCollection data)
        {
            List<Customer> Conformation = new List<Customer>();
            var first = data["first"].Trim().TrimStart().TrimEnd();
            var last = data["last"].Trim().TrimStart().TrimEnd();
            var email = data["email"].Trim().TrimStart().TrimEnd();
            var fee = data["fee"].Trim().TrimStart().TrimEnd();
            var pickup = data["pickup"].Trim().TrimStart().TrimEnd();
            var dropoff = data["dropoff"].Trim().TrimStart().TrimEnd();
            var mileage = data["mileage"].Trim().TrimStart().TrimEnd();
            var Id = data["Id"].Trim().TrimStart().TrimEnd();
            var prevmiles = data["carmilies"].Trim().TrimStart().TrimEnd();
            var conform = data["conformation"].Trim().TrimStart().TrimEnd();
            string type = "";

            Customer MakeReservation = new Customer();
            Reservation CheckReservation = new Reservation();

            switch (fee)
            {
                case "1":
                    fee = "19.95";
                    type = "Compact Vehicles";
                    break;
                case "2":
                    fee = "24.95";
                    type = "Standard Vehicles";
                    break;
                case "3":
                    fee = "39.00";
                    type = "Luxury Vehicles";
                    break;

            }

            double Fee = Convert.ToDouble(fee);
            double dayfee;
            double totalfee;
            DateTime pickingup = DateTime.ParseExact(pickup, "mm/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime dropingoff = DateTime.ParseExact(dropoff, "mm/dd/yyyy", CultureInfo.InvariantCulture);
            TimeSpan difference = dropingoff - pickingup;
            var days = difference.TotalDays;
            if (days > 0)
            {

                dayfee = Fee * days;
            }
            else
            {
                dayfee = Fee * 1;
            }

            double MileNow = Convert.ToInt32(mileage);
            double MilePre = Convert.ToInt32(prevmiles);
            double TotalMile = MileNow + MilePre;
            string MileString = Convert.ToString(TotalMile);


            double totalmiles = MileNow * .32;
            totalfee = dayfee + totalmiles;
            
          
            fee = Convert.ToString(totalfee);


            MakeReservation.CustomerFirstName = first;
            MakeReservation.CustomerLastName = last;
            MakeReservation.CustomerEmail = email;
            MakeReservation.CustomerPickUp = pickup;
            MakeReservation.CustomerDropOff = dropoff;
            MakeReservation.CarType = type;
            MakeReservation.CarToRentId = Id;
            MakeReservation.CarFee = fee;
            MakeReservation.Mileage = MileString;

          

            if ((CheckReservation.UpdateCarsAvailable(MakeReservation.CarToRentId) == true) && (CheckReservation.CreateReservation(MakeReservation) == true) && (CheckReservation.UpdateMileage(MakeReservation.Mileage, MakeReservation.CarToRentId) == true))
            {
                Conformation.Add(MakeReservation);
                ViewBag.Conformation = Conformation;
                string Display = "Your Registertion Number to return the car is " + conform + ".";

                ViewBag.AlertMsgC = Display;
                return RedirectToAction("Conformation");

            }
            else
            {
                ViewBag.AlertMsgC = "";
                return RedirectToAction("ShowAllCars");

            }

        }

     
        public ActionResult ReturnCar()
        {
            return View();
        }

        public ActionResult ProcessConformation( string conformation)
        {
            Reservation UnReservation = new Reservation();
            List<string> msg = new List<string>();
            
            string conform = conformation.Trim().TrimStart().TrimEnd();
            if (!string.IsNullOrEmpty(conform))
            {

                if (UnReservation.ReturnCar(conform) == true)
                {
                    ViewBag.AlertMsgP = null;
                }
                else if(UnReservation.ReturnCar(conform) == false)
                {
                    msg.Add(" " + conform + " is not the Registertion Number to return this car!");
                    ViewBag.AlertMsgP = msg;
                    return RedirectToAction("ReturnCar");
                }

            }
            else
            {
                msg.Add("There was nothing entered");
                ViewBag.AlertMsgP = msg;
                return RedirectToAction("ReturnCar");
            }
            return RedirectToAction("Index");

        }


    }
}