
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;


namespace RetroFlyreiser.Controllers
{
    public class HomeController : Controller
    {

        private bool CheckSession()
        {
            if (Session["LoggetInn"] == null)
            {
                return false;
            }
            else if ((bool)Session["LoggetInn"] == false)
            {
                return false;
            }
            return true;
        }


        public ActionResult VisForside()
        {
            var db = new FlyplassBLL();
            ViewBag.Flyplasser = db.alleFlyplasser();
            return View(ViewBag);
        }

        public ActionResult Index()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }
        
       
        public ActionResult VisViewModel()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            DbModel VM = new DbModel();
            try
            {
                var dbRuter = new RuteBLL();
                var dbFlymaskiner = new FlymaskinBLL();
                var dbFlyplasser = new FlyplassBLL();
                var dbKunder = new KundeBLL();
                var dbPoststeder = new PoststedBLL();
                var dbBestillinger = new BestillingBLL();
                var dbBrukere = new BrukerBLL();

                VM.Ruter = dbRuter.alleRuter();
                VM.Flymaskiner = dbFlymaskiner.alleFlymaskiner();
                VM.Flyplasser = dbFlyplasser.alleFlyplasser();
                VM.Kunder = dbKunder.alleKunder();
                VM.Poststeder = dbPoststeder.allePoststeder();
                VM.Bestillinger = dbBestillinger.alleBestillinger();
                VM.Brukere = dbBrukere.alleBrukere();
                return View(VM);
            }
            catch(Exception ex)
            {
                LogError(ex);
                return View("~/Views/Error/Error.cshtml");
            }
        }
        
    }
}