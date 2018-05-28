using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using CoffeeTest.Data.Commands;
using CoffeeTest.Data.DTO;
using CoffeeTest.Data.Queries;
using CoffeeTest.Domain;

namespace CoffeeTest.Controllers
{
    public class PantryIngredientController : Controller
    {
        // GET: PantryIngredient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStockIngredientForSelectedPantry(int id)
        {
            var model = new PantryIngredientQueries().GetIngredientsLeftOfSelectedPantry(id);

            return PartialView("_TotalUnitsLeftPartial", model);
        }

        public JsonResult AddIngredientsToOffice(int milk, int sugar, int coffee, int officeId)
        {
            var success = new OfficeIngredientCommands().AddIngredientToOffice(sugar, coffee, milk, officeId);
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddDrinksModal(int pantryId)
        {
            var model = new OfficeIngredientCommands().AddDrinkModal(pantryId);
            return PartialView("_AddDrinksModal", model);
        }

        public JsonResult AddDrinks(List<int> drinkIds, int pantryId)
        {
            var success = new PantryCommand().AddDrinkToPantry(drinkIds, pantryId);
            return Json(new {success}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewGraphOfOffice(int officeId)
        {
            var model = new OrderQueries().GetStocksLeftByOffice(officeId);
            return View(model);
        }

        public ActionResult ViewGraphOfOfficeStockPartial(int officeId)
        {
            var model = new OrderQueries().GetStocksLeftByOffice(officeId);
            return PartialView("_ViewGraphOfOfficeStockPartial", model);
        }

        public ActionResult ViewGraphOfOfficeUnitsPartial(int officeId)
        {
            var model = new OrderQueries().GetStocksLeftByOffice(officeId);
            return PartialView("_ViewGraphOfOfficeUnitsPartial", model);
        }
    }
}