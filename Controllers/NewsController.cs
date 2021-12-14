using System.Web.Mvc;
using GrainMaster.BuisnessLogic;

namespace GrainMaster.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Get(string CName)
        {
            if (UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(News.GetNewsDetail(CName));
        }
        public ViewResult GetAll()
        {
            return View("../News/Get",News.GetAllNews());
        }
    }
}