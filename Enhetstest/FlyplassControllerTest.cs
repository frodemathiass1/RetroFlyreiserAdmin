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
    public class FlyplassControllerTest
    {

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlyplass(innFlyplass);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlyplass(innFlyplass);

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void ListFlyplasser_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<Flyplass>();
            var flyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };


            forventetResultat.Add(flyplass);
            forventetResultat.Add(flyplass);
            forventetResultat.Add(flyplass);

            // Act
            var actionResult = (ViewResult)controller.ListFlyplasser();
            var resultat = (List<Flyplass>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].FlyplassKode, resultat[i].FlyplassKode);
                Assert.AreEqual(forventetResultat[i].By, resultat[i].By);
            }
        }

        [TestMethod]
        public void ListFlyplasser_Session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListFlyplasser();

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlyplass();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Registrer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlyplass();

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerFlyplass(innFlyplass);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListFlyplasser");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass();
            controller.ViewData.ModelState.AddModelError("flyplassKode", "Ikke oppgitt flyplassKode");

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlyplass(innFlyplass);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass();
            innFlyplass.FlyplassKode = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerFlyplass(innFlyplass);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettFlyplass()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettFlyplass("");
            var resultat = (Flyplass)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettFlyplass_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act                        
            var result = (RedirectToRouteResult)controller.SlettFlyplass("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");

        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo",

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettFlyplass("OSL", innFlyplass);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListFlyplasser");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo",

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettFlyplass("OSL", innFlyplass);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettFlyplass("", innFlyplass);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            // Act
            var actionResult = (ViewResult)controller.FlyplassDetaljer("OSL");
            var resultat = (Flyplass)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.FlyplassKode, resultat.FlyplassKode);
            Assert.AreEqual(forventetResultat.By, resultat.By);
        }

        [TestMethod]
        public void Detaljer_Session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            // Act
            var actionResult = (RedirectToRouteResult)controller.FlyplassDetaljer("");

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreFlyplass("");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreFlyplass("");

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreFlyplass("");
            var flyplassResultat = (Flyplass)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(flyplassResultat.FlyplassKode, "TST");
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };

            // Act
            var actionResult = (ViewResult)controller.EndreFlyplass("", innFlyplass);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass();
            controller.ViewData.ModelState.AddModelError("feil", "FlyplassKode = ''");

            // Act
            var actionResult = (ViewResult)controller.EndreFlyplass("", innFlyplass);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "FlyplassKode = ''");
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreFlyplass("OSL", innFlyplass);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListFlyplasser");
        }

        [TestMethod]
        public void Endre_funnet_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new FlyplassController(new FlyplassBLL(new FlyplassStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innFlyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreFlyplass("OSL", innFlyplass);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "IngenTilgang");
        }



    }
}