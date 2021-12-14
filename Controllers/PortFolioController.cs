using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GrainMaster.Models;

namespace GrainMaster.Controllers
{
    public class PortFolioController : Controller
    {
        // GET: PortFolio
        [HttpGet]
        public ActionResult CreatePortFolio()
        {
            if(UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(PortFolio.GetByID());
        }
        [HttpPost]
        public ActionResult CreatePortFolio([Bind(Exclude = "ID")] FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PortFolioModel portFolioModel = new PortFolioModel()
                    {
                        StockName = Convert.ToString(formCollection["PortFolio.StockName"]),
                        Quantity = Convert.ToString(formCollection["PortFolio.Quantity"]),
                        Price = Convert.ToString(formCollection["PortFolio.Price"]),
                        Date = Convert.ToDateTime(formCollection["PortFolio.Date"]),
                        TPSID = Convert.ToString(formCollection["PortFolio.TPSID"]),
                        PurchaseType = Convert.ToInt32(formCollection["ddlPurchaseType"])
                    };
                    string msg = PortFolio.CreatePortFolio(portFolioModel);
                    if (msg != "")
                        TempData["portfolioMSg"] = msg;
                }
                return View(PortFolio.GetByID());
            }
            catch (Exception ex)
            {
                TempData["portfolioMSg"] = ex.Message.ToString();
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult DeletePortFolio(int id)
        {
            try
            {
                PortFolio.Delete(id);
                return RedirectToAction("CreatePortFolio", "PortFolio");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        

        [HttpPost]
        public JsonResult UpdatePortFolio(PortFolioModel portFolioModel)
        {
            try
            {
                return Json(PortFolio.Edit(portFolioModel), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public JsonResult StockDetail(string Prefix)
        {
            try
            {
                List<StockDetail> ObjList = PortFolio.GetSector();
                var result = from N in ObjList
                            where N.Name.ToLower().StartsWith(Prefix.ToLower())
                            select new { N.Name,N.ID };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        [HttpPost]
        public JsonResult GetCompanyTP(string CName)
        {
            try
            {
                return Json(PortFolio.GetTP(CName), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
