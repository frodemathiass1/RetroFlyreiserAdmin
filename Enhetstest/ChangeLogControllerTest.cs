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
    public class ChangeLogControllerTest
    {

        [TestMethod]
        public void Session_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ChangeLogController(new ChangeLogBLL(new ChangeLogStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = null;

            // Act
            var result = (RedirectToRouteResult)controller.ListChangeLogger();


            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }


        [TestMethod]
        public void Session_false()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ChangeLogController(new ChangeLogBLL(new ChangeLogStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListChangeLogger();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }

        [TestMethod]
        public void ListBestilling_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ChangeLogController(new ChangeLogBLL(new ChangeLogStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = true;

            var dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var changeLogListe = new List<ChangeLog>();
            var changeLog = new ChangeLog()
            {
                Id = 1,
                EntityName = "EntityName",
                PropertyName = "PropertyName",
                PrimaryKeyValue = "PK",
                OldValue = "OldValue",
                NewValue = "NewValue",
                DateChanged = dateTime,

            };


            // Act
            var actionResult = (ViewResult)controller.ListChangeLogger();
            var resultat = (List<ChangeLog>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(resultat[i].Id, resultat[i].Id);
                Assert.AreEqual(resultat[i].EntityName, resultat[i].EntityName);
                Assert.AreEqual(resultat[i].PropertyName, resultat[i].PropertyName);
                Assert.AreEqual(resultat[i].PrimaryKeyValue, resultat[i].PrimaryKeyValue);
                Assert.AreEqual(resultat[i].OldValue, resultat[i].OldValue);
                Assert.AreEqual(resultat[i].NewValue, resultat[i].NewValue);
                Assert.AreEqual(resultat[i].DateChanged, resultat[i].DateChanged);
            }
        }

        [TestMethod]
        public void ListBestilling_OK_session_ikke_OK()
        {

            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ChangeLogController(new ChangeLogBLL(new ChangeLogStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggetInn"] = false;

            // Act
            var result = (RedirectToRouteResult)controller.ListChangeLogger();

            // Assert          
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "IngenTilgang");
        }
    }
}
