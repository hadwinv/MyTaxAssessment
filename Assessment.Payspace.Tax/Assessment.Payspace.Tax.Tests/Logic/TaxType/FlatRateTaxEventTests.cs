using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Logic.Core.TaxType;
using Moq;
using NUnit.Framework;
using System;

namespace Assessment.Payspace.Tax.Tests.Logic.TaxType
{
    [TestFixture]
    public class FlatRateTaxEventTests
    {
        private Mock<ITaxDataAccess> _taxDataAccess;
        private FlatRateTaxEvent _flatRateTaxEvent;
        
        [SetUp]
        public void Init()
        {
            _taxDataAccess = new Mock<ITaxDataAccess>();
            _flatRateTaxEvent = new FlatRateTaxEvent(_taxDataAccess.Object);
            //moxk test data
            _taxDataAccess.Setup(x => x.GetFlatTaxRate())
               .Returns(new Dto.DataAccess.FlatTaxRate
               {
                   FlatTaxRateId = 1,
                   Percentage = 17.5m,
                   UserCreated = "FlatRateTest",
                   DateCreated = DateTime.Now

               });
        }
        
        [Test]
        public void Assess_Amount_At_Seventeen_Point_Five_Percent_When_Flat_Rate_Calculation()
        {
            decimal result = 0;

            result = _flatRateTaxEvent.TaxCalculator(50000);
            //assert if the 17.5% was applied to amount
            Assert.AreEqual(8750, result);
        }
    }
}
