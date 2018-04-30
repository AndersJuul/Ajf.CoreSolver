using System.Net;
using System.Web.Http.Results;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Tests.Base;
using Ajf.CoreSolver.WebApi.Controllers;
using AutoFixture;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ajf.CoreSolver.Tests.WebApi.Controllers
{
    [TestFixture]
    public class ValuesControllerTest:BaseUnitTest
    {
        //[Test]
        //public void Get()
        //{
        //    // Arrange
        //    CalculationController controller = new CalculationController();

        //    // Act
        //    IEnumerable<string> result = controller.Get();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count());
        //    Assert.AreEqual("value1", result.ElementAt(0));
        //    Assert.AreEqual("value2", result.ElementAt(1));
        //}

        //[Test]
        //public void GetById()
        //{
        //    // Arrange
        //    CalculationController controller = new CalculationController();

        //    // Act
        //    string result = controller.Get(5);

        //    // Assert
        //    Assert.AreEqual("value", result);
        //}

        [Test]
        public void Post()
        {
            // Arrange
            var calculationRequest = Fixture.Create<CalculationRequest>();
            var calculationRequestValidator = Fixture.Create<ICalculationRequestValidator>();
            var validationResult = Fixture
                .Build<ValidationResult>()
                .With(x => x.IsValid, true)
                .Create();
            calculationRequestValidator
                .Stub(x => x.Validate(calculationRequest))
                .Return(validationResult);
            var controller = new CalculationController(calculationRequestValidator);

            // Act
            var response = controller.Post(calculationRequest);

            // Assert
            // TODO replace with what it really is
            Assert.IsTrue(response is OkNegotiatedContentResult<CalculationResponse>);
        }

        //[Test]
        //public void Put()
        //{
        //    // Arrange
        //    CalculationController controller = new CalculationController();

        //    // Act
        //    controller.Put(5, "value");

        //    // Assert
        //}

        //[Test]
        //public void Delete()
        //{
        //    // Arrange
        //    CalculationController controller = new CalculationController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}