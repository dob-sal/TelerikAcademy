using BullsAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BullsAndCows.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var db = new BullsAndCowsDbContext();
            db.SaveChanges();


            return View();
        }
    }
}
