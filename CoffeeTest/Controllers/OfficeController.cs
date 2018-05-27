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
    public class OfficeController : Controller
    {
        // GET: Office
        public ActionResult Index()
        {
            var model = new OfficeQueries().GetAllOffices();
            return View(model);
        }

        public ActionResult OfficeEditor(int? id)
        {
            var model = new OfficeDto();

            if (!id.HasValue)
            {
                model = new OfficeDto();
            }
            else
            {
                model = new OfficeQueries().GetSelectedOffice(id.Value);
            }

            return View(model);
        }

        public ActionResult AddOffice(OfficeDto office)
        {
            try
            {
                var addedoffice = new OfficeCommands().AddOffice(office);
                return RedirectToAction("OfficeEditor", new { id = addedoffice.OfficeId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.GetBaseException();
                return RedirectToAction("OfficeEditor");
            }
        }

        public ActionResult EditOffice(OfficeDto office)
        {
            try
            {
                var editedOffice = new OfficeCommands().EditOffice(office);
                return RedirectToAction("OfficeEditor", new { edit = true, officeId = editedOffice.OfficeId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.GetBaseException();
                return RedirectToAction("OfficeEditor");
            }
        }

        public ActionResult GetOfficePantries(int officeId)
        {
            var model = new OfficeQueries().GetSelectedOfficePantriesAndDrinks(officeId);
            return PartialView("_OfficePantriesPartial", model);
        }
    }
}