using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Logic.Core.TaxType;
using Moq;
using NUnit.Framework;
using System;

namespace Assessment.Payspace.Tax.Tests.Logic.TaxType
{
    [TestFixture]
    public class FlatValueTaxEventTests
    {
        private Mock<ITaxDataAccess> _taxDataAccess;
        private FlatValueTaxEvent _flatValueTaxEvent;

        [SetUp]
        public void Init()
        {
            _taxDataAccess = new Mock<ITaxDataAccess>();
            _flatValueTaxEvent = new FlatValueTaxEvent(_taxDataAccess.Object);
            //mock test data
            _taxDataAccess.Setup(x => x.GetFlatValueTaxRate())
               .Returns(new Dto.DataAccess.FlatValueTaxRate
               {
                   FlatValueTaxRateId = 1,
                   Percentage = 5m,
                   Value = 10000m,
                   MaximumAmount = 200000m,
                   UserCreated = "FlatValueTest",
                   DateCreated = DateTime.Now
               });
        }
        
        [Test]
        public void Assess_Amount_Less_Than_TwoHundredThousand_When_Flat_Value_Calculation()
        {
            decimal result = 0;

            result = _flatValueTaxEvent.TaxCalculator(120000);
            //assert if the 5% was applied to amount
            Assert.AreEqual(6000, result);
        }

        [Test]
        public void Assess_Amount_Equals_TwoHundredThousand_When_Flat_Value_Calculation()
        {
            decimal result = 0;

            result = _flatValueTaxEvent.TaxCalculator(200000);
            //boundary check i.e. 200000 results in 10000
            Assert.AreEqual(10000, result);
        }

        [Test]
        public void Assess_Amount_More_Than_TwoHundredThousand_When_Flat_Value_Calculation()
        {
            decimal result = 0;

            result = _flatValueTaxEvent.TaxCalculator(250000);
            //assert that taxable amount 10000 if more than 199999.99
            Assert.AreEqual(10000, result);
        }
    }
}
