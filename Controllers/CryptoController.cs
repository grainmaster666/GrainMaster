using System.Web.Mvc;
using GrainMaster.BuisnessLogic;
using GrainMaster.Models;

namespace GrainMaster.Controllers
{
    public class CryptoController : Controller
    {
        public ActionResult GetCrypto()
        {
            if (UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(Crypto.Get());
        }
    }
}