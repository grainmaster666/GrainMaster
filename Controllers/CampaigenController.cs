using GrainMaster.BuisnessLogic;
using GrainMaster.Models;
using System.Web.Mvc;

namespace GrainMaster.Controllers
{
    
    public class CampaigenController : Controller
    {

        [HttpPost]
        public ActionResult InsertCampaigen(CampaigenModel cryptoModel)
        {
            return Json(Campaigen.SaveCampaigen(cryptoModel), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCampaigen()
        {
            if (UserLogic.LoggedUser.Name == null)
            {
              return RedirectToAction("Login", "Login");
            }
            return View(Campaigen.Get());
        }

        public ActionResult Delete(int id)
        {
            Campaigen.DeleteCamp(id);
            ViewBag.Messsage = "Record Deleted Successfully!!";
            return RedirectToAction("GetCampaigen");
        }
    }
}