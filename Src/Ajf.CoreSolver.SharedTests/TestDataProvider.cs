using System;
using Ajf.CoreSolver.Models.External;
using AutoFixture;

namespace Ajf.CoreSolver.SharedTests
{
    public static class TestDataProvider
    {
        private static readonly Fixture Fixture = new Fixture();

        public static Unit CreateValidRootUnit()
        {
            return Fixture
                .Build<Unit>()
                .With(x => x.SubUnits, new[]
                {
                    CreateValidUnit(),
                    CreateValidUnit(),
                    CreateValidUnit(),
                    CreateValidUnit()
                })
                .Create();
        }

        public static Unit CreateValidUnit()
        {
            return Fixture
                .Build<Unit>()
                .With(x => x.Id, Guid.NewGuid())
                .With(x => x.SubUnits, new Unit[] { })
                .Create();
        }

        public static CalculationRequest GetValidCalculationRequest()
        {
            var unit = CreateValidRootUnit();
            var calculationRequest = Fixture
                .Build<CalculationRequest>()
                .With(x => x.Unit, unit)
                .With(x => x.AlgorithmSelector, "COUNTERCLOCKWISE")
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