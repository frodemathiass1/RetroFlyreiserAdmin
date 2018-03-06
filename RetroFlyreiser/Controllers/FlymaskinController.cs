using RetroFlyreiser.BLL;
using RetroFlyreiser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroFlyreiser.Controllers
{
    public class FlymaskinController : Controller
    {
        private IFlymaskinBLL _flymaskinBLL;

        public FlymaskinController()
        {
            _flymaskinBLL = new FlymaskinBLL();
        }

        public FlymaskinController(IFlymaskinBLL stub)
        {
            _flymaskinBLL = stub;
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

        public ActionResult SlettFlymaskin(string FlyId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Flymaskin enFlymaskin = _flymaskinBLL.hentFlymaskin(FlyId);
            return View(enFlymaskin);
        }

        [HttpPost]
        public ActionResult SlettFlymaskin(string FlyId, Flymaskin innFlymaskin)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _flymaskinBLL.slettFlymaskin(FlyId);
            if (OK)
            {
                return RedirectToAction("ListFlymaskiner");
            }
            return View("~/Views/Error/Error.cshtml");
        }

        public ActionResult ListFlymaskiner()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var alleFlymaskiner = _flymaskinBLL.alleFlymaskiner().ToList();
            return View(alleFlymaskiner);
        }

        public ActionResult FlymaskinDetaljer(string FlyId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var flymaskin = _flymaskinBLL.hentFlymaskin(FlyId);
            return View(flymaskin);
        }

        public ActionResult EndreFlymaskin(string FlyId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Flymaskin enFlymaskin = _flymaskinBLL.hentFlymaskin(FlyId);
            return View(enFlymaskin);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreFlymaskin(string FlyId, Flymaskin endreFlymaskin)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool OK = _flymaskinBLL.endreFlymaskin(FlyId, endreFlymaskin);
                if (OK)
                {
                    return RedirectToAction("ListFlymaskiner");
                }
            }
            return View();
        }

        public ActionResult RegistrerFlymaskin()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerFlymaskin(Flymaskin innFlymaskin)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool insertOK = _flymaskinBLL.settInnFlymaskin(innFlymaskin);
                if (insertOK)
                {
                    return RedirectToAction("ListFlymaskiner");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }
    }
}