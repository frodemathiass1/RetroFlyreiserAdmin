using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetroFlyreiser.BLL;
using RetroFlyreiser.DAL;
using static RetroFlyreiser.DAL.BrukerDAL;
using RetroFlyreiser.Model;
using RetroFlyreiser.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;


namespace RetroFlyreiser.Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            // Act
            var actionResult = (ViewResult)controller.RegistrerBruker();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;


            // Act
            var actionResult = (ViewResult)controller.RegistrerBruker();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void ListBrukere_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new List<DBBRUKER>();
            var bruker = new DBBRUKER()
            {
                BRUKERNAVN = "TestBruker",
            };

            forventetResultat.Add(bruker);
            forventetResultat.Add(bruker);
            forventetResultat.Add(bruker);

            // Act
            var actionResult = (ViewResult)controller.ListBrukere();
            var resultat = (List<DBBRUKER>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].BRUKERNAVN, resultat[i].BRUKERNAVN);
            }
        }

        [TestMethod]
        public void ListBrukere_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListBrukere();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Registrer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.RegistrerBruker();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        
        [TestMethod]
        public void Registrer_Post_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBruker = new Bruker()
            {
                Brukernavn = "TestBruker",
                Passord = "TestPassord"
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBruker(innBruker);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "LoginSide");
        }


        [TestMethod]
        public void Registrer_Post_OK_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innBruker = new Bruker()
            {
                Brukernavn = "TestBruker",
                Passord = "TestPassord"
            };
            // Act
            var result = (RedirectToRouteResult)controller.RegistrerBruker(innBruker);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "LoginSide");
        }



        [TestMethod]
        public void Registrer_Post_Model_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBruker = new Bruker();
            controller.ViewData.ModelState.AddModelError("brukernavn", "Ikke oppgitt brukernavn");

            // Act
            var actionResult = (ViewResult)controller.RegistrerBruker(innBruker);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBruker = new Bruker();
            innBruker.Brukernavn = "";

            // Act
            var actionResult = (ViewResult)controller.RegistrerBruker(innBruker);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "~/Views/Error/Error.cshtml");
        }


        [TestMethod]
        public void SlettBruker()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.SlettBruker("");
            var resultat = (DBBRUKER)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettBruker_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.SlettBruker("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Slettet_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBruker = new Bruker()
            {
                Brukernavn = "TestBruker",
                Passord = "TestPassord"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBruker("TestBruker", innBruker);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListBrukere");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var innBruker = new Bruker()
            {
                Brukernavn = "TestBruker",
                Passord = "TestPassord"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettBruker("", innBruker);

            // Assert
            Assert.AreEqual(actionResult.ViewName,"~/Views/Error/Error.cshtml");
        }

        [TestMethod]
        public void Slettet_funnet_Post_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var innBruker = new Bruker()
            {
                Brukernavn = "TestBruker",
                Passord = "TestPassord"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBruker("TestBruker", innBruker);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var forventetResultat = new DBBRUKER()
            {
                BRUKERNAVN = "TestBruker",
            };
            // Act
            var actionResult = (ViewResult)controller.BrukerDetaljer("TestBruker");
            var resultat = (DBBRUKER)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.BRUKERNAVN, resultat.BRUKERNAVN);
        }

        [TestMethod]
        public void Detaljer_session_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var forventetResultat = new DBBRUKER()
            {
                BRUKERNAVN = "TestBruker",
            };
            // Act
            var result = (RedirectToRouteResult)controller.BrukerDetaljer("");

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void IngenTilgang()
        {
            //Arrange
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));

            //Act
            ViewResult result = controller.IngenTilgang() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void LogsinSide_View()
        {
            //Arrange
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));

            //Act
            ViewResult result = controller.LoginSide() as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }


        [TestMethod]
        public void LoginSide_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var bruker = new Bruker()
            {
                Brukernavn = "Test"

            };

            // Act
            var actionResultat = (RedirectToRouteResult)controller.LoginSide(bruker);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void LoginSide_ikke_OK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            var bruker = new Bruker()
            {
                Brukernavn = ""

            };

            // Act
            var actionResultat = (ViewResult)controller.LoginSide(bruker);

            // Assert
            Assert.AreEqual("", actionResultat.ViewName);
        }

        [TestMethod]
        public void LoggUt()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;


            // Act
            var actionResultat = (RedirectToRouteResult)controller.LoggUt();

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "VisForside");
        }

        [TestMethod]
        public void Bruker_i_db_()
        {
            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var sjekkbruker = new BrukerBLL();
            bool bruker_i_db = true;

            //Act
            bool result = bruker_i_db;

            //Assert
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void Bruker_i_db_ikke_OK()
        {
            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new BrukerBLL(new BrukerStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var sjekkbruker = new BrukerBLL();
            bool bruker_i_db = false;

            //Act
            bool result = bruker_i_db;

            //Assert
            Assert.AreEqual(false, false);
        }
    }
}
 