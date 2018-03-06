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
    public class PoststedControllerTest
    {

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerPoststed();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.RegistrerPoststed();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void ListPoststed_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<Poststed>();
            var poststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };

            forventetResultat.Add(poststed);
            forventetResultat.Add(poststed);
            forventetResultat.Add(poststed);

            // Act
            var actionResult = (ViewResult)controller.ListPoststeder();
            var resultat = (List<Poststed>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Postnr, resultat[i].Postnr);
                Assert.AreEqual(forventetResultat[i].Sted, resultat[i].Sted);
            }
        }

        [TestMethod]
        public void ListPoststed_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListPoststeder();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }



        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerPoststed();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerPoststed(innPoststed);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListPoststeder");
        }

        [TestMethod]
        public void Registrer_Post_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerPoststed(innPoststed);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed();
            controller.ViewData.ModelState.AddModelError("postnr", "Ikke oppgitt postnr");

            // Act
            var actionResult = (ViewResult)controller.RegistrerPoststed(innPoststed);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed();
            innPoststed.Postnr = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerPoststed(innPoststed);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettPoststed()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettPoststed("");
            var resultat = (Poststed)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettPoststed_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettPoststed("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettPoststed("1000", innPoststed);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListPoststeder");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettPoststed("1000", innPoststed);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettPoststed("", innPoststed);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var actionResult = (ViewResult)controller.PoststedDetaljer("1000");
            var resultat = (Poststed)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.Postnr, resultat.Postnr);
            Assert.AreEqual(forventetResultat.Sted, resultat.Sted);
        }

        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var result = (RedirectToRouteResult)controller.PoststedDetaljer("1000");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndrePoststed("");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_Session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.EndrePoststed("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void Endre_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndrePoststed("");
            var poststedResultat = (Poststed)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(poststedResultat.Postnr, "0000");
        }

        [TestMethod]
        public void Endre_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };

            // Act
            var actionResult = (ViewResult)controller.EndrePoststed("", innPoststed);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_feil_validering_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed();
            controller.ViewData.ModelState.AddModelError("feil", "Postnr = ''");

            // Act
            var actionResult = (ViewResult)controller.EndrePoststed("", innPoststed);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "Postnr = ''");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndrePoststed("1000", innPoststed);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListPoststeder");
        }

        [TestMethod]
        public void Endre_funnet_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new PoststedController(new PoststedBLL(new PoststedStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innPoststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndrePoststed("1000", innPoststed);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}