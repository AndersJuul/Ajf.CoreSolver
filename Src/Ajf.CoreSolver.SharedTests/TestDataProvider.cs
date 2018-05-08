using Ajf.CoreSolver.Models.External;
using AutoFixture;

namespace Ajf.CoreSolver.SharedTests
{
    public static class TestDataProvider
    {
        private static readonly Fixture Fixture = new Fixture();

        public static Unit CreateValidUnit()
        {
            return Fixture
                .Build<Unit>()
                .With(x => x.SubUnits, new[]
                {
                    new Unit(),
                    new Unit(),
                    new Unit(),
                    new Unit()
                })
                .Create();
        }

        public static CalculationRequest GetValidCalculationRequest()
        {
            var unit = CreateValidUnit();
            var calculationRequest = Fixture
                .Build<CalculationRequest>()
                .With(x => x.Unit, unit)
                .Create();
            return calculationRequest;
        }

        public static CalculationRequest GetInvalidCalculationRequest()
        {
            var unit = CreateInvalidUnit();
            var calculationRequest = Fixture
                .Build<CalculationRequest>()
                .With(x => x.Unit, unit)
                .Create();
            return calculationRequest;
        }

        private static Unit CreateInvalidUnit()
        {
            return null;
        }
    }
}