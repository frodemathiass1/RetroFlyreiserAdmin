using RetroFlyreiser.BLL;
using RetroFlyreiser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetroFlyreiser.Controllers
{
    public class ChangeLogController : Controller
    {
        private IChangeLogBLL _changeLogBLL;

        public ChangeLogController()
        {
            _changeLogBLL = new ChangeLogBLL();
        }

        public ChangeLogController(IChangeLogBLL stub)
        {
            _changeLogBLL = stub;
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

        public ActionResult ListChangeLogger()
        {
            if (!CheckSession())
            {
                return RedirectToAction("IngenTilgang", "Admin");
            }
            List<ChangeLog> alleChangeLogger = _changeLogBLL.alleChangeLogger();
            return View(alleChangeLogger);
        }
    }
}