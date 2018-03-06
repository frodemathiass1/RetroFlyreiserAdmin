using RetroFlyreiser.BLL;
using RetroFlyreiser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroFlyreiser.Controllers
{
    public class FlyplassController : Controller
    {
        private IFlyplassBLL _flyplassBLL;

        public FlyplassController()
        {
            _flyplassBLL = new FlyplassBLL();
        }
        
        public FlyplassController(IFlyplassBLL stub)
        {
            _flyplassBLL = stub;
        }

        public bool CheckSession()
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


        public ActionResult ListFlyplasser()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            List<Flyplass> alleFlyplasser = _flyplassBLL.alleFlyplasser();
            return View(alleFlyplasser);
        }


        public ActionResult SlettFlyplass(string flyplassKode)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Flyplass enFlyplass = _flyplassBLL.hentFlyplass(flyplassKode);
            return View(enFlyplass);
        }

        [HttpPost]
        public ActionResult SlettFlyplass(string flyplassKode, Flyplass flyplass)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _flyplassBLL.slettFlyplass(flyplassKode);
            if (OK)
            {
                return RedirectToAction("ListFlyplasser");
            }
            return View("~/Views/Error/Error.cshtml");
        }

        public ActionResult FlyplassDetaljer(string FlyplassKode)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var flyplass = _flyplassBLL.hentFlyplass(FlyplassKode);
            return View(flyplass);
        }

        public ActionResult EndreFlyplass(string FlyplassKode)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Flyplass enFlyplass = _flyplassBLL.hentFlyplass(FlyplassKode);
            return View(enFlyplass);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreFlyplass(string FlyplassKode, Flyplass endreFlyplass)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool OK = _flyplassBLL.endreFlyplass(FlyplassKode, endreFlyplass);
                if (OK)
                {
                    return RedirectToAction("ListFlyplasser");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }

        public ActionResult RegistrerFlyplass()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerFlyplass(Flyplass innFlyplass)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool insertOK = _flyplassBLL.settInnFlyplass(innFlyplass);
                if (insertOK)
                {
                    return RedirectToAction("ListFlyplasser");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }
    }
}