
using System.Web.Mvc;
using CoffeeTest.Data.Commands;
using CoffeeTest.Data.Queries;

namespace CoffeeTest.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            var model = new OrderQueries().GetAllOrders();
            return View(model);
        }

        public JsonResult OrderDrink(int pantryId, int drinkId)
        {
            var success = new OrderCommands().OrderDrinks(drinkId, pantryId);
            return Json(new {success}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewRemainingPerOffice(int officeId)
        {
            var model = new OrderQueries().OrdersHistoryPerOffice();
            return View(model);
        }

        public ActionResult ViewOrderDistribution()
        {
            var model = new DrinkQueries().GetAllCountOfDrinks();
            return PartialView("_ViewOrderDistributionPartial", model);
        }

        public ActionResult DrinkDistribution()
        {
            return View();
        }
    }
}