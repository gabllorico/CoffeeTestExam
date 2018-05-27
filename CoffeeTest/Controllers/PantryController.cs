using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTest.Data.Queries;

namespace CoffeeTest.Controllers
{
    public class PantryController : Controller
    {
        // GET: Pantry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PantryEditor(int? id)
        {
            return View();
        }
    }
}