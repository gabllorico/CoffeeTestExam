using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeTest.Controllers
{
    public class PantryIngredientController : Controller
    {
        // GET: PantryIngredient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStockIngredientForSelectedPantry(int pantryId)
        {
            return PartialView("");
        }
    }
}