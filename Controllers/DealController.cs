using GrainMaster.BuisnessLogic;
using System.Web.Mvc;

namespace GrainMaster.Controllers
{
    public class DealController : Controller
    {
        // GET: Deal
        public ActionResult GetDeal()
        {
            if (UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(Deal.Get());
        }
    }
}