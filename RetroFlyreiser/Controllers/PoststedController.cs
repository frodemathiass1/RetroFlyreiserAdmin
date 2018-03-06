using System.Collections.Generic;
using System.Web.Mvc;
using RetroFlyreiser.Model;
using RetroFlyreiser.BLL;

namespace RetroFlyreiser.Controllers
{
    public class PoststedController : Controller
    {
        private IPoststedBLL _poststedBLL;

        public PoststedController()
        {
            _poststedBLL = new PoststedBLL();
        }

        public PoststedController(IPoststedBLL stub)
        {
            _poststedBLL = stub;
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


            public ActionResult ListPoststeder()
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                List<Poststed> allePoststeder = _poststedBLL.allePoststeder();
                return View(allePoststeder);
            }


            public ActionResult EndrePoststed(string postnr)
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                Poststed etPoststed = _poststedBLL.hentPoststed(postnr);
                return View(etPoststed);
            }


            [ValidateAntiForgeryToken]
            [HttpPost]
            public ActionResult EndrePoststed(string Postnr, Poststed endrePoststed)
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                if (ModelState.IsValid)
                {
                    bool OK = _poststedBLL.endrePoststed(Postnr, endrePoststed);
                    if (OK)
                    {
                        return RedirectToAction("ListPoststeder");
                    }
                }
                return View();
            }


            public ActionResult SlettPoststed(string postnr)
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                Poststed etPoststed = _poststedBLL.hentPoststed(postnr);
                return View(etPoststed);
            }


             [HttpPost]
             public ActionResult SlettPoststed(string postnr, Poststed innPoststed)
             {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                bool OK = _poststedBLL.slettPoststed(postnr);
                if (OK)
                {
                    return RedirectToAction("ListPoststeder");
                }
                return View("~/Views/Error/Error.cshtml");
            }


            public ActionResult PoststedDetaljer(string postnr)
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                var poststed = _poststedBLL.hentPoststed(postnr);
                return View(poststed);
            }


            public ActionResult RegistrerPoststed()
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                return View();
            }


            [ValidateAntiForgeryToken]
            [HttpPost]
            public ActionResult RegistrerPoststed(Poststed innPoststed)
            {
                if (!CheckSession())
                {
                    return RedirectToAction("IngenTilgang", "Admin");
                }
                if (ModelState.IsValid)
                {
                    bool insertOK = _poststedBLL.settInnPoststed(innPoststed);
                    if (insertOK)
                    {
                        return RedirectToAction("ListPoststeder");
                    }
                }
                return View("~/Views/Error/Error.cshtml");
            }
        }
    }
