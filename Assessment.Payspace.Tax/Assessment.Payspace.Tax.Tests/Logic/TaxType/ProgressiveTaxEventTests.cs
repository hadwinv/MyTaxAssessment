using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Logic.Core.TaxType;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Tests.Logic.TaxType
{
    [TestFixture]
    public class ProgressiveTaxEventTests
    {
        private Mock<ITaxDataAccess> _taxDataAccess;
        private ProgressiveTaxEvent _progressiveTaxEvent;

        [SetUp]
        public void Init()
        {
            _taxDataAccess = new Mock<ITaxDataAccess>();
            _progressiveTaxEvent = new ProgressiveTaxEvent(_taxDataAccess.Object);
            //mock test data
            _taxDataAccess.Setup(x => x.GetProgressiveTaxRates())
                .Returns(new List<Dto.DataAccess.ProgressiveTaxRate>
                {
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 1,
                        Percentage = 10,
                        MinimumAmount = 0,
                        MaximumAmount = 8350.00m
                    },
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 2,
                        Percentage = 15,
                        MinimumAmount = 8351.00m,
                        MaximumAmount = 33950.00m
                    },
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 3,
                        Percentage = 25,
                        MinimumAmount = 33951.00m,
                        MaximumAmount = 82250.00m
                    },
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 4,
                        Percentage = 28,
                        MinimumAmount = 82251.00m,
                         MaximumAmount = 171550.00m
                    },
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 5,
                        Percentage = 33,
                        MinimumAmount = 171551.00m,
                        MaximumAmount = 372950.00m
                    },
                    new Dto.DataAccess.ProgressiveTaxRate
                    {
                        ProgressiveTaxRateId = 6,
                        Percentage = 35,
                        MinimumAmount = 372951.00m,
                        MaximumAmount = (int?)null
                    }
                });
        }

        [Test]
        public void Assess_Tax_Amount_At_TenPercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(7000);
            //assert if the 10% was applied to amount
            Assert.AreEqual(700, result);
        }

        [Test]
        public void Assess_Tax_Amount_At_FifteenPercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(15000);
            //assert if the 10%.15% was applied to amount
            Assert.AreEqual(1832.5m, result);
        }

        [Test]
        public void Assess_Tax_Amount_At_TwentyFivePercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(50000);
            //assert if the 10%.15%,25% was applied to amount
            Assert.AreEqual(8687.6m, result);
        }

        [Test]
        public void Assess_Tax_Amount_At_TwentyEightPercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(120000);
            //assert if the 10%.15%,25%,28% was applied to amount
            Assert.AreEqual(27320.16m, result);
        }

        [Test]
        public void Assess_Tax_Amount_At_ThirtyThreePercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(250000);
            //assert if the 10%.15%,25%,28%.33% was applied to amount
            Assert.AreEqual(67642.81m, result);
        }

        [Test]
        public void Assess_Tax_Amount_At_ThirtyFivePercentage_When_Progressive_Calculation()
        {
            decimal result = 0;

            result = _progressiveTaxEvent.TaxCalculator(500000);
            //assert if the 10%.15%,25%,28%.33%,35% was applied to amount
            Assert.AreEqual(152683.89m, result);
        }
    }
}
