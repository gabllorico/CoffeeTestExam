using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeTest.Data.Commands;
using CoffeeTest.Data.DTO;
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

        public ActionResult PantryEditor(int? id, int officeId)
        {
            var model = new PantryWithOfficeIdDto
            {
                OfficeId = officeId
            };

            if (id.HasValue)
                model = new PantryQueries().GetSelectedPantry(id.Value);

            return View(model);
        }

        public ActionResult GetPantryDrinks(int id)
        {
            var model = new PantryQueries().GetDrinksOfSelectedPantry(id);

            return PartialView("_PantryDrinksPartial", model);
        }

        public ActionResult AddPantry(PantryWithOfficeIdDto pantry)
        {
            try
            {
                var addedPantry = new PantryCommand().AddPantry(pantry);
                return RedirectToAction("PantryEditor", "Pantry", new { id = addedPantry.PantryId, officeId = addedPantry.OfficeId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.GetBaseException();
                return RedirectToAction("PantryEditor", "Pantry", new { officeId = pantry.OfficeId });
            }

        }

        public JsonResult OrderDrinkFromSelectedPantry(int pantryId, int drinkId)
        {
            var success = new OrderCommands().OrderDrinks(drinkId, pantryId);
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddDrinksModal(int pantryId)
        {
            var model = new OfficeIngredientCommands().AddDrinkModal(pantryId);
            return PartialView("_AddDrinksModal", model);
        }
    }
}