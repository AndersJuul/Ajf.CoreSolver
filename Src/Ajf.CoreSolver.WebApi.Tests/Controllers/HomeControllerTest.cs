using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ajf.CoreSolver.WebApi;
using Ajf.CoreSolver.WebApi.Controllers;

namespace Ajf.CoreSolver.WebApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
