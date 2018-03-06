using System.Collections.Generic;
using System.Web.Mvc;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;


namespace RetroFlyreiser.Controllers
{
    public class AdminController : Controller
    {
        private IBrukerBLL _brukerBLL;

        public AdminController()
        {
            _brukerBLL = new BrukerBLL();
        }

        public AdminController(IBrukerBLL stub)
        {
            _brukerBLL = stub;
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


        public ActionResult IngenTilgang()
        {
            return View();
        }

        public ActionResult ListBrukere()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            List<DBBRUKER> alleBrukere = _brukerBLL.alleBrukere();
            return View(alleBrukere);
        }


        public ActionResult LoginSide()
        {

            return View();
        }


        [HttpPost]
        public ActionResult LoginSide(Bruker innBruker)
        {
            bool eksistererBruker = _brukerBLL.bruker_i_db(innBruker);     
            if (eksistererBruker)
            {
                Session["LoggetInn"] = true;
                return RedirectToAction("Index","Home");
            }
            else
            {
                Session["LoggetInn"] = false;
                return View();   
            }
        }

        public ActionResult LoggUt()
        {
            Session.Clear();
            return RedirectToAction("VisForside","Home");
        }


        public ActionResult RegistrerBruker()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegistrerBruker(Bruker innBruker)
        {
            if (ModelState.IsValid)
            {
                bool insertOK = _brukerBLL.settInnBruker(innBruker);
                if (insertOK)
                {
                    return RedirectToAction("LoginSide");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult SlettBruker(string brukernavn)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            DBBRUKER enFlyplass = _brukerBLL.hentEnBruker(brukernavn);
            return View(enFlyplass);
        }

        [HttpPost]
        public ActionResult SlettBruker(string Brukernavn, Bruker innBruker)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _brukerBLL.slettBruker(Brukernavn);
            if (OK)
            {
                return RedirectToAction("ListBrukere");
            }
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult BrukerDetaljer(string Brukernavn)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            DBBRUKER bruker = _brukerBLL.hentEnBruker(Brukernavn);
            return View(bruker);
        }

    }
}

