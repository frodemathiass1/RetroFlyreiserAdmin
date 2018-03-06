using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;

namespace RetroFlyreiser.Controllers
{
    public class BestillingController : Controller
    {
        private IBestillingBLL _bestillingBLL;

        public BestillingController()
        {
            _bestillingBLL = new BestillingBLL();
        }

        public BestillingController(IBestillingBLL stub)
        {
            _bestillingBLL = stub;
        }


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

        public ActionResult ListBestillinger()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            List<Bestilling> alleBestillinger = _bestillingBLL.alleBestillinger();
            return View(alleBestillinger);
        }

        public ActionResult EndreBestilling(int Id)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Bestilling enBestilling = _bestillingBLL.hentEnBestilling(Id);
            return View(enBestilling);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreBestilling(int Id, Bestilling endreBestilling)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool OK = _bestillingBLL.endreBestilling(Id, endreBestilling);
                if (OK)
                {
                    return RedirectToAction("ListBestillinger");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }

        public ActionResult SlettBestilling(int Id)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Bestilling enBestilling = _bestillingBLL.hentEnBestilling(Id);
            return View(enBestilling);

        }


        [HttpPost]
        public ActionResult SlettBestilling(int Id, Bestilling innBestilling)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _bestillingBLL.slettBestilling(Id);
            if (OK)
            {
                return RedirectToAction("ListBestillinger");
            }
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult BestillingDetaljer(int Id)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var bestilling = _bestillingBLL.hentEnBestilling(Id);

            return View(bestilling);
        }


        public ActionResult RegistrerBestilling()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerBestilling(Bestilling innBestilling)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool insertOK = _bestillingBLL.settInnBestilling(innBestilling);
                if (insertOK)
                {
                    return RedirectToAction("ListBestillinger");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }

    }
}