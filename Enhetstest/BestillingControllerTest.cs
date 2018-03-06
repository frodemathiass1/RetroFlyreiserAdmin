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
    public class BestillingControllerTest
    {

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            var innBestilling = new Bestilling()
            {
                Id = 1
            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBestilling();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub())); ;
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innBestilling = new Bestilling()
            {
                Id = 1

            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBestilling();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void ListBestilling_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };

            var forventetResultat = new List<Bestilling>();
            var bestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            forventetResultat.Add(bestilling);
            forventetResultat.Add(bestilling);
            forventetResultat.Add(bestilling);

            // Act
            var actionResult = (ViewResult)controller.ListBestillinger();
            var resultat = (List<Bestilling>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Id, resultat[i].Id);
                Assert.AreEqual(forventetResultat[i].Rute.RuteId, resultat[i].Rute.RuteId);
                Assert.AreEqual(forventetResultat[i].Rute.ReiseFra.FlyplassKode, resultat[i].Rute.ReiseFra.FlyplassKode);
                Assert.AreEqual(forventetResultat[i].Rute.ReiseFra.By, resultat[i].Rute.ReiseFra.By);
                Assert.AreEqual(forventetResultat[i].Rute.ReiseTil.FlyplassKode, resultat[i].Rute.ReiseTil.FlyplassKode);
                Assert.AreEqual(forventetResultat[i].Rute.ReiseTil.By, resultat[i].Rute.ReiseTil.By);
                Assert.AreEqual(forventetResultat[i].Rute.Dato, resultat[i].Rute.Dato);
                Assert.AreEqual(forventetResultat[i].Rute.Tid, resultat[i].Rute.Tid);
                Assert.AreEqual(forventetResultat[i].Rute.ReiseTid, resultat[i].Rute.ReiseTid);
                Assert.AreEqual(forventetResultat[i].Rute.Flymaskin.FlyId, resultat[i].Rute.Flymaskin.FlyId);
                Assert.AreEqual(forventetResultat[i].Rute.Flymaskin.Type, resultat[i].Rute.Flymaskin.Type);
                Assert.AreEqual(forventetResultat[i].Rute.Flymaskin.Kapasitet, resultat[i].Rute.Flymaskin.Kapasitet);
                Assert.AreEqual(forventetResultat[i].Rute.Pris, resultat[i].Rute.Pris);

                Assert.AreEqual(forventetResultat[i].Kunde.Fornavn, resultat[i].Kunde.Fornavn);
                Assert.AreEqual(forventetResultat[i].Kunde.Etternavn, resultat[i].Kunde.Etternavn);
                Assert.AreEqual(forventetResultat[i].Kunde.Adresse, resultat[i].Kunde.Adresse);
                Assert.AreEqual(forventetResultat[i].Kunde.Poststed.Postnr, resultat[i].Kunde.Poststed.Postnr);
                Assert.AreEqual(forventetResultat[i].Kunde.Poststed.Sted, resultat[i].Kunde.Poststed.Sted);
                Assert.AreEqual(forventetResultat[i].Kunde.Telefon, resultat[i].Kunde.Telefon);
                Assert.AreEqual(forventetResultat[i].Kunde.Epost, resultat[i].Kunde.Epost);
                Assert.AreEqual(forventetResultat[i].Kunde.Aktiv, resultat[i].Kunde.Aktiv);

            }
        }

        [TestMethod]
        public void ListBestilling_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListBestillinger();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }



        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerBestilling();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBestilling(innBestilling);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListBestillinger");
        }

        [TestMethod]
        public void Registrer_Post_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBestilling(innBestilling);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBestilling = new Bestilling();
            controller.ViewData.ModelState.AddModelError("id", "Ikke oppgitt id");

            // Act
            var actionResult = (ViewResult)controller.RegistrerBestilling(innBestilling);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBestilling = new Bestilling();
            innBestilling.Id = 0;

            // Act
            var actionResult = (ViewResult)controller.RegistrerBestilling(innBestilling);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettBestilling()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettBestilling(0);
            var resultat = (Bestilling)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void SlettBestilling_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettBestilling(0);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;
            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBestilling(1, innBestilling);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListBestillinger");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;
            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBestilling(1, innBestilling);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResult = (ViewResult)controller.SlettBestilling(0, innBestilling);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var forventetResultat = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };
            // Act
            var actionResult = (ViewResult)controller.BestillingDetaljer(1);
            var resultat = (Bestilling)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.Id, resultat.Id);
            Assert.AreEqual(forventetResultat.Rute.RuteId, resultat.Rute.RuteId);
            Assert.AreEqual(forventetResultat.Rute.ReiseFra.FlyplassKode, resultat.Rute.ReiseFra.FlyplassKode);
            Assert.AreEqual(forventetResultat.Rute.ReiseFra.By, resultat.Rute.ReiseFra.By);
            Assert.AreEqual(forventetResultat.Rute.ReiseTil.FlyplassKode, resultat.Rute.ReiseTil.FlyplassKode);
            Assert.AreEqual(forventetResultat.Rute.ReiseTil.By, resultat.Rute.ReiseTil.By);
            Assert.AreEqual(forventetResultat.Rute.Dato, resultat.Rute.Dato);
            Assert.AreEqual(forventetResultat.Rute.Tid, resultat.Rute.Tid);
            Assert.AreEqual(forventetResultat.Rute.ReiseTid, resultat.Rute.ReiseTid);
            Assert.AreEqual(forventetResultat.Rute.Flymaskin.FlyId, resultat.Rute.Flymaskin.FlyId);
            Assert.AreEqual(forventetResultat.Rute.Flymaskin.Type, resultat.Rute.Flymaskin.Type);
            Assert.AreEqual(forventetResultat.Rute.Flymaskin.Kapasitet, resultat.Rute.Flymaskin.Kapasitet);
            Assert.AreEqual(forventetResultat.Rute.Pris, resultat.Rute.Pris);
            Assert.AreEqual(forventetResultat.Kunde.Fornavn, resultat.Kunde.Fornavn);
            Assert.AreEqual(forventetResultat.Kunde.Etternavn, resultat.Kunde.Etternavn);
            Assert.AreEqual(forventetResultat.Kunde.Adresse, resultat.Kunde.Adresse);
            Assert.AreEqual(forventetResultat.Kunde.Poststed.Postnr, resultat.Kunde.Poststed.Postnr);
            Assert.AreEqual(forventetResultat.Kunde.Poststed.Sted, resultat.Kunde.Poststed.Sted);
            Assert.AreEqual(forventetResultat.Kunde.Telefon, resultat.Kunde.Telefon);
            Assert.AreEqual(forventetResultat.Kunde.Epost, resultat.Kunde.Epost);
            Assert.AreEqual(forventetResultat.Kunde.Aktiv, resultat.Kunde.Aktiv);
        }

        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var forventetResultat = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };
            // Act
            var result = (RedirectToRouteResult)controller.BestillingDetaljer(1);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreBestilling(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.EndreBestilling(1);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreBestilling(0);
            var bestillingResultat = (Bestilling)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(bestillingResultat.Id, 0);
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResult = (ViewResult)controller.EndreBestilling(0, innBestilling);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBestilling = new Bestilling();
            controller.ViewData.ModelState.AddModelError("feil", "Id = 0");

            // Act
            var actionResult = (ViewResult)controller.EndreBestilling(0, innBestilling);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "Id = 0");
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreBestilling(1, innBestilling);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListBestillinger");
        }

        [TestMethod]
        public void Endre_funnet_Session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new BestillingController(new BestillingBLL(new BestillingStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Bestilling Objekter
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };
            ;
            var innBestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
            };

            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreBestilling(1, innBestilling);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}