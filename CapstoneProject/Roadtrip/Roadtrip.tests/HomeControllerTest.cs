using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Roadtrip;
using Roadtrip.Controllers;

namespace Roadtrip.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        //Example test case, checks to see if the about view returns non-null
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.About() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Contact()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result); 
        }
    }

    [TestClass]
    public class RoutesControllerTest
    {
        [TestMethod]
        public void HowTo()
        {
            RoutesController controller = new RoutesController();
            ViewResult result = controller.HowTo() as ViewResult;
            Assert.IsNotNull(result);
        }
    }

}