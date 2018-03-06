using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetroFlyreiser.BLL;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;
using RetroFlyreiser.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;


namespace RetroFlyreiser.Enhetstest
{
    [TestClass]
    public class RuteControllerTest
    {

        // Flyplasser og FLymaskin objekter til Rute - OK
        private Flyplass reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
        private Flyplass reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
        private Flymaskin flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };

        // Flyplasser og FLymaskin objekter til Rute - Feil
        private Flyplass reiseFraFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
        private Flyplass reiseTilFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
        private Flymaskin flymaskinFeil = new Flymaskin() { FlyId = "TS00", Type = "Feil", Kapasitet = 1 };

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerRute();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerRute();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void ListRuter_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<Rute>();
            var rute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };


            forventetResultat.Add(rute);
            forventetResultat.Add(rute);
            forventetResultat.Add(rute);

            // Act
            var actionResult = (ViewResult)controller.ListRuter();
            var resultat = (List<Rute>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].RuteId, resultat[i].RuteId);
                Assert.AreEqual(forventetResultat[i].ReiseFra.FlyplassKode, resultat[i].ReiseFra.FlyplassKode);
                Assert.AreEqual(forventetResultat[i].ReiseFra.By, resultat[i].ReiseFra.By);
                Assert.AreEqual(forventetResultat[i].ReiseTil.FlyplassKode, resultat[i].ReiseTil.FlyplassKode);
                Assert.AreEqual(forventetResultat[i].ReiseTil.By, resultat[i].ReiseTil.By);
                Assert.AreEqual(forventetResultat[i].Dato, resultat[i].Dato);
                Assert.AreEqual(forventetResultat[i].Tid, resultat[i].Tid);
                Assert.AreEqual(forventetResultat[i].ReiseTid, resultat[i].ReiseTid);
                Assert.AreEqual(forventetResultat[i].Flymaskin.FlyId, resultat[i].Flymaskin.FlyId);
                Assert.AreEqual(forventetResultat[i].Flymaskin.Type, resultat[i].Flymaskin.Type);
                Assert.AreEqual(forventetResultat[i].Flymaskin.Kapasitet, resultat[i].Flymaskin.Kapasitet);
                Assert.AreEqual(forventetResultat[i].Pris, resultat[i].Pris);

            }
        }

        [TestMethod]
        public void ListRuter_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListRuter();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");

        }


        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerRute();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerRute(innRute);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListRuter");
        }

        [TestMethod]
        public void Registrer_Post_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerRute(innRute);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute();
            controller.ViewData.ModelState.AddModelError("ruteId", "Ikke oppgitt ruteId");

            // Act
            var actionResult = (ViewResult)controller.RegistrerRute(innRute);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute();
            innRute.RuteId = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerRute(innRute);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettRute()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettRute("");
            var resultat = (Rute)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettRute_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettRute("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettRute("OSLKRS000", innRute);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListRuter");
        }


        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettRute("OSLKRS000", innRute);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };

            // Act
            var actionResult = (ViewResult)controller.SlettRute("", innRute);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var actionResult = (ViewResult)controller.RuteDetaljer("OSLKRS000");
            var resultat = (Rute)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.RuteId, resultat.RuteId);
            Assert.AreEqual(forventetResultat.ReiseFra.By, resultat.ReiseFra.By);
            Assert.AreEqual(forventetResultat.ReiseFra.FlyplassKode, resultat.ReiseFra.FlyplassKode);
            Assert.AreEqual(forventetResultat.ReiseTil.By, resultat.ReiseTil.By);
            Assert.AreEqual(forventetResultat.ReiseTil.FlyplassKode, resultat.ReiseTil.FlyplassKode);
            Assert.AreEqual(forventetResultat.Dato, resultat.Dato);
            Assert.AreEqual(forventetResultat.Tid, resultat.Tid);
            Assert.AreEqual(forventetResultat.ReiseTid, resultat.ReiseTid);
            Assert.AreEqual(forventetResultat.Flymaskin.FlyId, resultat.Flymaskin.FlyId);
            Assert.AreEqual(forventetResultat.Flymaskin.Type, resultat.Flymaskin.Type);
            Assert.AreEqual(forventetResultat.Flymaskin.Kapasitet, resultat.Flymaskin.Kapasitet);
            Assert.AreEqual(forventetResultat.Pris, resultat.Pris);
        }


        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var result = (RedirectToRouteResult)controller.RuteDetaljer("OSLKRS000");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreRute("");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.EndreRute("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreRute("");
            var flyplassResultat = (Rute)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(flyplassResultat.RuteId, "TSTTST000");
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };

            // Act
            var actionResult = (ViewResult)controller.EndreRute("", innRute);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute();
            controller.ViewData.ModelState.AddModelError("feil", "RuteId = ''");

            // Act
            var actionResult = (ViewResult)controller.EndreRute("", innRute);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "RuteId = ''");
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreRute("OSLKRS000", innRute);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListRuter");
        }

        [TestMethod]
        public void Endre_funnet_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new RuteController(new RuteBLL(new RuteStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innRute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreRute("OSLKRS000", innRute);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}