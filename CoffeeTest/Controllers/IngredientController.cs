using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTest.Data.Queries;

namespace CoffeeTest.Controllers
{
    public class IngredientController : Controller
    {
        // GET: Ingredient
        public ActionResult Index()
        {
            var model = new IngredientQueries().GetAllIngredients();
            return View(model);
        }
    }
}
