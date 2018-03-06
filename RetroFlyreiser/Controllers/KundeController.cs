using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetroFlyreiser;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;


namespace RetroFlyreiser.Controllers
{

    public class KundeController : Controller
    {
        private IKundeBLL _kundeBLL;

        public KundeController()
        {
            _kundeBLL = new KundeBLL();
        }

        public KundeController(IKundeBLL stub)
        {
            _kundeBLL = stub;
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

        public ActionResult ListKunder()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            List<Kunde> alleKunder = _kundeBLL.alleKunder();
            return View(alleKunder);
        }

        public ActionResult EndreKunde(string Epost)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Kunde enKunde = _kundeBLL.hentKunde(Epost);
            return View(enKunde);
        }
 

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreKunde(string Epost, Kunde endreKunde)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool OK = _kundeBLL.endreKunde(Epost, endreKunde);
                if (OK)
                {
                    return RedirectToAction("ListKunder");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult KundeDetaljer(string Epost)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var kunde = _kundeBLL.hentKunde(Epost);
            return View(kunde);
        }


        public ActionResult RegistrerKunde()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerKunde(Kunde innKunde)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool insertOK = _kundeBLL.settInnKunde(innKunde);
                if (insertOK)
                {
                    return RedirectToAction("ListKunder");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }

        public ActionResult SlettKunde(string epost)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Kunde enKunde = _kundeBLL.hentKunde(epost);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult SlettKunde(string Epost, Kunde innKunde)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _kundeBLL.slettKunde(Epost);
            if (OK)
            {
                return RedirectToAction("ListKunder");
            }
            return View("~/Views/Error/Error.cshtml");
        }

    }
}