using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTest.Data.Queries;

namespace CoffeeTest.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index(int officeId, int pantryId)
        {
            var model = new DrinkQueries().GetAllDrinks();
            return View(model);
        }
    }
}