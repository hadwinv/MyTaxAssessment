using Assessment.Payspace.Tax.Dto;
using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Interface.Logic;
using Assessment.Payspace.Tax.Logic;
using Moq;
using NUnit.Framework;
using System;

namespace Assessment.Payspace.Tax.Tests.Logic
{
    [TestFixture]
    public class TaxLogicTest
    {
        private Mock<ITaxDataAccess> _taxDataAccess;
        private ITaxLogic _taxLogic;

        [SetUp]
        public void Init()
        {
            _taxDataAccess = new Mock<ITaxDataAccess>();
            _taxLogic = new TaxLogic(_taxDataAccess.Object);
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

            _taxDataAccess.Setup(x => x.GetTaxTypeByPostalCode(It.IsAny<string>()))
               .Returns(new Dto.DataAccess.TaxType
               {
                   TaxTypeId = 3,
                   PostalCode = "7000",
                   CalculationType = "Flat Rate"
               });
        }

        [Test]
        public void Verify_That_TaxAmount_Is_Returned()
        {
            decimal result = 0;
            //set up query and call logic
            result = _taxLogic.TaxAssessment(
                new TaxQuery
                {
                    PostalCode = "7000",
                    Amount = 7000
                });
            //assert that insert method was call
            Assert.IsInstanceOf(typeof(decimal), result);
        }

        [Test]
        public void Verify_That_Tax_Assement_Is_Inserted ()
        {
            //set up query and call logic
            _taxLogic.TaxAssessment(
                new TaxQuery
                {
                    PostalCode = "7000",
                    Amount = 7000
                });

            //assert that insert method was call
            _taxDataAccess.Verify(x => x.AddTaxAssessment(It.IsAny<TaxAssessment>()));
        }
    }
}
