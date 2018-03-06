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
    public class FlymaskinerControllerTest
    {

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            var innflymaskin = new Flymaskin()
            {
                FlyId = "OSL",
                Kapasitet = 100,
                Type = "Boeing 737"
            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlymaskin(innflymaskin);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            var innflymaskin = new Flymaskin()
            {
                FlyId = "OSL",
                Kapasitet = 100,
                Type = "Boeing 737"
            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlymaskin(innflymaskin);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }



        [TestMethod]
        public void ListFlymaskiner_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<Flymaskin>();
            var flymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };


            forventetResultat.Add(flymaskin);
            forventetResultat.Add(flymaskin);
            forventetResultat.Add(flymaskin);

            // Act
            var actionResult = (ViewResult)controller.ListFlymaskiner();
            var resultat = (List<Flymaskin>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].FlyId, resultat[i].FlyId);
                Assert.AreEqual(forventetResultat[i].Type, resultat[i].Type);
                Assert.AreEqual(forventetResultat[i].Kapasitet, resultat[i].Kapasitet);
            }
        }

        [TestMethod]
        public void ListFlymaskiner_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListFlymaskiner();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlymaskin();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Registrer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlymaskin();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlymaskin(innFlymaskin);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListFlymaskiner");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin();
            controller.ViewData.ModelState.AddModelError("flyId", "Ikke oppgitt flyId");

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlymaskin(innFlymaskin);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }



        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin();
            innFlymaskin.FlyId = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlymaskin(innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettFLymaskin()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettFlymaskin("");
            var resultat = (Flymaskin)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettFLymaskin_Session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettFlymaskin("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");

        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettFlymaskin("AA00", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListFlymaskiner");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettFlymaskin("", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };

            // Act
            var actionResult = (ViewResult)controller.SlettFlymaskin("", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            // Act
            var actionResult = (ViewResult)controller.FlymaskinDetaljer("AA00");
            var resultat = (Flymaskin)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.FlyId, resultat.FlyId);
            Assert.AreEqual(forventetResultat.Type, resultat.Type);
            Assert.AreEqual(forventetResultat.Kapasitet, resultat.Kapasitet);
        }

        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            // Act            
            var actionResult = (RedirectToRouteResult)controller.FlymaskinDetaljer("");

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreFlymaskin("");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreFlymaskin("");

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreFlymaskin("");
            var flyplassResultat = (Flymaskin)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(flyplassResultat.FlyId, "TST");
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };

            // Act
            var actionResult = (ViewResult)controller.EndreFlymaskin("", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin();
            controller.ViewData.ModelState.AddModelError("feil", "FlyId = ''");

            // Act
            var actionResult = (ViewResult)controller.EndreFlymaskin("", innFlymaskin);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "FlyId = ''");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreFlymaskin("AA00", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListFlymaskiner");
        }

        [TestMethod]
        public void Endre_funnet_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlymaskinController(new FlymaskinBLL(new FlymaskinStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innFlymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreFlymaskin("", innFlymaskin);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}