using System.Web.Mvc;
using GrainMaster.Models;

namespace GrainMaster.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                RegisterModel registerModel = new RegisterModel
                {
                    Name= formCollection["Name"],
                    Email = formCollection["Email"],
                    Password = formCollection["Password"]
                };
                bool isSuccess = new Register().UserRegister(registerModel);
                if (isSuccess)
                    ViewData["success"] = "User Registered Successfully";
                View();
            }
            return View();
        }
    }
}