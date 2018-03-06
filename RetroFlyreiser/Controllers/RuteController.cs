using System.Linq;
using System.Web.Mvc;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;
using System.Diagnostics;

namespace RetroFlyreiser.Controllers
{
    public class RuteController : Controller
    {
        private IRuteBLL _ruteBLL;

        public RuteController()
        {
            _ruteBLL = new RuteBLL();
        }

        public RuteController(IRuteBLL stub)
        {
            _ruteBLL = stub;
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

        public ActionResult ListRuter()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var alleRuter = _ruteBLL.alleRuter().ToList();
            return View(alleRuter);
        }


        public ActionResult SlettRute(string ruteId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Rute enRute = _ruteBLL.hentRute(ruteId);
            return View(enRute);
        }

        [HttpPost]
        public ActionResult SlettRute(string ruteId, Rute innRute)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            bool OK = _ruteBLL.slettRute(ruteId);
            if (OK)
            {
                return RedirectToAction("ListRuter");
            }
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult RuteDetaljer(string ruteId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            var rute = _ruteBLL.hentRute(ruteId);
            return View(rute);
        }


        public ActionResult EndreRute(string ruteId)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            Rute enRute = _ruteBLL.hentRute(ruteId);
            return View(enRute);
        }


        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreRute(string ruteId, Rute endreRute)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool OK = _ruteBLL.endreRute(ruteId, endreRute);
                if (OK)
                {
                    return RedirectToAction("ListRuter");
                }
            }
            Debug.Write(ModelState.IsValid);
            return View("~/Views/Error/Error.cshtml");
        }


        public ActionResult RegistrerRute()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerRute(Rute innRute)
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            if (ModelState.IsValid)
            {
                bool insertOK = _ruteBLL.settInnRute(innRute);
                if (insertOK)
                {
                    return RedirectToAction("ListRuter");
                }
            }
            return View("~/Views/Error/Error.cshtml");
        }

    }
}