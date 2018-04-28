using System.Collections.Generic;
using System.Linq;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.WebApi.Controllers;
using NUnit.Framework;

namespace Ajf.CoreSolver.WebApi.Tests.Controllers
{
    [TestFixture]
    public class ValuesControllerTest
    {
        [Test]
        public void Get()
        {
            // Arrange
            CalculationController controller = new CalculationController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [Test]
        public void GetById()
        {
            // Arrange
            CalculationController controller = new CalculationController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [Test]
        public void Post()
        {
            // Arrange
            CalculationController controller = new CalculationController();

            // Act
            controller.Post(new CalculationRequest());

            // Assert
        }

        [Test]
        public void Put()
        {
            // Arrange
            CalculationController controller = new CalculationController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [Test]
        public void Delete()
        {
            // Arrange
            CalculationController controller = new CalculationController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
