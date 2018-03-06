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
    public class KundeControllerTest
    {
        private Poststed poststed = new Poststed()
        {
            Postnr = "1000",
            Sted = "Oslo"
        };

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true

            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerKunde();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;


            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true

            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerKunde();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void ListKunder_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<Kunde>();
            var kunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);

            // Act
            var actionResult = (ViewResult)controller.ListKunder();
            var resultat = (List<Kunde>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Fornavn, resultat[i].Fornavn);
                Assert.AreEqual(forventetResultat[i].Etternavn, resultat[i].Etternavn);
                Assert.AreEqual(forventetResultat[i].Adresse, resultat[i].Adresse);
                Assert.AreEqual(forventetResultat[i].Poststed.Postnr, resultat[i].Poststed.Postnr);
                Assert.AreEqual(forventetResultat[i].Poststed.Sted, resultat[i].Poststed.Sted);
                Assert.AreEqual(forventetResultat[i].Telefon, resultat[i].Telefon);
                Assert.AreEqual(forventetResultat[i].Epost, resultat[i].Epost);

            }
        }

        [TestMethod]
        public void ListKunder_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListKunder();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerKunde();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerKunde(innKunde);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListKunder");
        }

        [TestMethod]
        public void Registrer_Post_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerKunde(innKunde);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde();
            controller.ViewData.ModelState.AddModelError("epost", "Ikke oppgitt epost");

            // Act
            var actionResult = (ViewResult)controller.RegistrerKunde(innKunde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde();
            innKunde.Epost = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerKunde(innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettKunde()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettKunde("");
            var resultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettKunde_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettKunde("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettKunde("unittest@test.no", innKunde);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListKunder");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettKunde("unittest@test.no", innKunde);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };

            // Act
            var actionResult = (ViewResult)controller.SlettKunde("", innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act
            var actionResult = (ViewResult)controller.KundeDetaljer("unittest@test.no");
            var resultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.Fornavn, resultat.Fornavn);
            Assert.AreEqual(forventetResultat.Etternavn, resultat.Etternavn);
            Assert.AreEqual(forventetResultat.Adresse, resultat.Adresse);
            Assert.AreEqual(forventetResultat.Poststed.Postnr, resultat.Poststed.Postnr);
            Assert.AreEqual(forventetResultat.Poststed.Sted, resultat.Poststed.Sted);
            Assert.AreEqual(forventetResultat.Telefon, resultat.Telefon);
            Assert.AreEqual(forventetResultat.Epost, resultat.Epost);
        }

        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act    
            var result = (RedirectToRouteResult)controller.KundeDetaljer("unittest@test.no");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreKunde("");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act            
            var result = (RedirectToRouteResult)controller.EndreKunde("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreKunde("");
            var flyplassResultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(flyplassResultat.Epost, "null@test.no");
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };

            // Act
            var actionResult = (ViewResult)controller.EndreKunde("", innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde();
            controller.ViewData.ModelState.AddModelError("feil", "Epost = ''");

            // Act
            var actionResult = (ViewResult)controller.EndreKunde("", innKunde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "Epost = ''");
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreKunde("unittest@test.no", innKunde);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListKunder");
        }

        [TestMethod]
        public void Endre_funnet_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innKunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            // Act
            var result = (RedirectToRouteResult)controller.EndreKunde("unittest@test.no", innKunde);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}