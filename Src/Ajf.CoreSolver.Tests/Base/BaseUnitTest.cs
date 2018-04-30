using AutoFixture;
using AutoFixture.AutoRhinoMock;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Base
{
    public abstract class BaseUnitTest
    {
        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoRhinoMockCustomization());
        }
    }
}