using Assessment.Payspace.Tax.DataAccess;
using Assessment.Payspace.Tax.DataAccess.EF;
using Assessment.Payspace.Tax.DataAccess.EF.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.Payspace.Tax.Tests.DataAccess
{
    [TestFixture]
    public class TaxDataAccessTest
    {
        private TaxDataAccess _taxDataAccess;
        private Mock<TaxAdministrationContext> _taxAdministrationContext;
        
        private Mock<DbSet<FlatTaxRate>> _mockFlatTaxRate;
        private List<FlatTaxRate> _flatTaxRates;
        private IQueryable<FlatTaxRate> _queryableFlatTaxRate;

        private Mock<DbSet<FlatValueTaxRate>> _mockFlatValueTaxRate;
        private List<FlatValueTaxRate> _flatValueTaxRates;
        private IQueryable<FlatValueTaxRate> _queryableFlatValueTaxRate;

        private Mock<DbSet<ProgressiveTaxRate>> _mockProgressiveTaxRate;
        private List<ProgressiveTaxRate> _progressiveTaxRates;
        private IQueryable<ProgressiveTaxRate> _queryableProgressiveTaxRate;

        private Mock<DbSet<TaxAssessment>> _mockTaxAssessment;
        private List<TaxAssessment> _taxAssessments;
        private IQueryable<TaxAssessment> _queryableTaxAssessment;

        private Mock<DbSet<TaxType>> _mockTaxType;
        private List<TaxType> _taxTypes;
        private IQueryable<TaxType> _queryableTaxType;

        private Mapper _mapper;

        [SetUp]
        public void Init()
        {
            _taxAdministrationContext = new Mock<TaxAdministrationContext>();
            //set up mock flat rate data
            _flatTaxRates = new List<FlatTaxRate>()
            {
                new FlatTaxRate
                {
                   FlatTaxRateId = 1,
                   Percentage = 17.5m,
                   UserCreated = "FlatRateTest",
                   DateCreated = DateTime.Now
                }
            };
            _queryableFlatTaxRate = _flatTaxRates.AsQueryable();

            //set up mock flat value data
            _flatValueTaxRates = new List<FlatValueTaxRate>()
            {
               new FlatValueTaxRate
               {
                   FlatValueTaxRateId = 1,
                   Percentage = 5m,
                   Value = 10000m,
                   MaximumAmount = 200000m,
                   UserCreated = "FlatValueTest",
                   DateCreated = DateTime.Now
               }
            };
            _queryableFlatValueTaxRate = _flatValueTaxRates.AsQueryable();

            //set up mock progressive rates data
            _progressiveTaxRates = new List<ProgressiveTaxRate>()
            {
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 1,
                    Percentage = 10,
                    MinimumAmount = 0,
                    MaximumAmount = 8350.00m,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                },
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 2,
                    Percentage = 15,
                    MinimumAmount = 8351.00m,
                    MaximumAmount = 33950.00m,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                },
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 3,
                    Percentage = 25,
                    MinimumAmount = 33951.00m,
                    MaximumAmount = 82250.00m,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                },
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 4,
                    Percentage = 28,
                    MinimumAmount = 82251.00m,
                    MaximumAmount = 171550.00m,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                },
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 5,
                    Percentage = 33,
                    MinimumAmount = 171551.00m,
                    MaximumAmount = 372950.00m,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                },
                new ProgressiveTaxRate
                {
                    ProgressiveTaxRateId = 6,
                    Percentage = 35,
                    MinimumAmount = 372951.00m,
                    MaximumAmount = (int?)null,
                    UserCreated = "ProgressiveTaxTest",
                    DateCreated = DateTime.Now
                }
            };
            _queryableProgressiveTaxRate = _progressiveTaxRates.AsQueryable();

            //set tax type data
            _taxTypes = new List<TaxType>()
            {
                new TaxType
                {
                   TaxTypeId = 1,
                   PostalCode = "7441",
                   CalculationType = "Progressive",
                   UserCreated = "TaxTypeTest",
                   DateCreated = DateTime.Now
                },
                new TaxType
                {
                   TaxTypeId = 1,
                   PostalCode = "A100",
                   CalculationType = "Flat Value",
                   UserCreated = "TaxTypeTest",
                   DateCreated = DateTime.Now
                },
                new TaxType
                {
                   TaxTypeId = 1,
                   PostalCode = "7000",
                   CalculationType = "",
                   UserCreated = "Flat Rate",
                   DateCreated = DateTime.Now
                },
                new TaxType
                {
                   TaxTypeId = 1,
                   PostalCode = "1000",
                   CalculationType = "Progressive",
                   UserCreated = "TaxTypeTest",
                   DateCreated = DateTime.Now
                }


            };
            _queryableTaxType = _taxTypes.AsQueryable();

            //set tax assessment data
            _taxAssessments = new List<TaxAssessment>()
            {
                new TaxAssessment
                {
                    TaxAssessmentId = 1,
                    NettIncome = 5000,
                    IncomeTax = 500,
                    TaxTypeId = 1,
                    UserCreated = "TaxAssessmentInsertTest",
                    DateCreated = DateTime.Now
                }
            };
            _queryableTaxAssessment = _taxAssessments.AsQueryable();
        }

        [Test]
        public void Return_Instance_Of_FlatTaxRate_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<FlatTaxRate, Dto.DataAccess.FlatTaxRate>()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockFlatTaxRate = new Mock<DbSet<FlatTaxRate>>();
            _mockFlatTaxRate.As<IQueryable<FlatTaxRate>>().Setup(m => m.Provider).Returns(_queryableFlatTaxRate.Provider);
            _mockFlatTaxRate.As<IQueryable<FlatTaxRate>>().Setup(m => m.Expression).Returns(_queryableFlatTaxRate.Expression);
            _mockFlatTaxRate.As<IQueryable<FlatTaxRate>>().Setup(m => m.ElementType).Returns(_queryableFlatTaxRate.ElementType);
            _mockFlatTaxRate.As<IQueryable<FlatTaxRate>>().Setup(m => m.GetEnumerator()).Returns(_queryableFlatTaxRate.GetEnumerator);

            _taxAdministrationContext.Setup(m => m.FlatTaxRate).Returns(_mockFlatTaxRate.Object);

            Assert.IsInstanceOf<Dto.DataAccess.FlatTaxRate> (_taxDataAccess.GetFlatTaxRate());
        }

        [Test]
        public void Return_Instance_Of_FlatValueTaxRate_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<FlatValueTaxRate, Dto.DataAccess.FlatValueTaxRate>()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockFlatValueTaxRate = new Mock<DbSet<FlatValueTaxRate>>();
            _mockFlatValueTaxRate.As<IQueryable<FlatValueTaxRate>>().Setup(m => m.Provider).Returns(_queryableFlatValueTaxRate.Provider);
            _mockFlatValueTaxRate.As<IQueryable<FlatValueTaxRate>>().Setup(m => m.Expression).Returns(_queryableFlatValueTaxRate.Expression);
            _mockFlatValueTaxRate.As<IQueryable<FlatValueTaxRate>>().Setup(m => m.ElementType).Returns(_queryableFlatValueTaxRate.ElementType);
            _mockFlatValueTaxRate.As<IQueryable<FlatValueTaxRate>>().Setup(m => m.GetEnumerator()).Returns(_queryableFlatValueTaxRate.GetEnumerator);

            _taxAdministrationContext.Setup(m => m.FlatValueTaxRate).Returns(_mockFlatValueTaxRate.Object);

            Assert.IsInstanceOf<Dto.DataAccess.FlatValueTaxRate>(_taxDataAccess.GetFlatValueTaxRate());
        }

        [Test]
        public void Return_List_Of_ProgressiveTaxRates_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<ProgressiveTaxRate, Dto.DataAccess.ProgressiveTaxRate>()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockProgressiveTaxRate = new Mock<DbSet<ProgressiveTaxRate>>();
            _mockProgressiveTaxRate.As<IQueryable<ProgressiveTaxRate>>().Setup(m => m.Provider).Returns(_queryableProgressiveTaxRate.Provider);
            _mockProgressiveTaxRate.As<IQueryable<ProgressiveTaxRate>>().Setup(m => m.Expression).Returns(_queryableProgressiveTaxRate.Expression);
            _mockProgressiveTaxRate.As<IQueryable<ProgressiveTaxRate>>().Setup(m => m.ElementType).Returns(_queryableProgressiveTaxRate.ElementType);
            _mockProgressiveTaxRate.As<IQueryable<ProgressiveTaxRate>>().Setup(m => m.GetEnumerator()).Returns(_queryableProgressiveTaxRate.GetEnumerator);

            _taxAdministrationContext.Setup(m => m.ProgressiveTaxRate).Returns(_mockProgressiveTaxRate.Object);

            Assert.IsTrue(_taxDataAccess.GetProgressiveTaxRates().Count > 0);
        }

        [Test]
        public void Return_List_Of_TaxTypes_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<TaxType, Dto.DataAccess.TaxType>()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockTaxType = new Mock<DbSet<TaxType>>();
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.Provider).Returns(_queryableTaxType.Provider);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.Expression).Returns(_queryableTaxType.Expression);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.ElementType).Returns(_queryableTaxType.ElementType);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.GetEnumerator()).Returns(_queryableTaxType.GetEnumerator);

            _taxAdministrationContext.Setup(m => m.TaxType).Returns(_mockTaxType.Object);

            Assert.IsTrue(_taxDataAccess.GetTaxTypes().Count > 0);
        }

        [Test]
        public void Return_Instance_Of_TaxType_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<TaxType, Dto.DataAccess.TaxType>()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockTaxType = new Mock<DbSet<TaxType>>();
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.Provider).Returns(_queryableTaxType.Provider);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.Expression).Returns(_queryableTaxType.Expression);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.ElementType).Returns(_queryableTaxType.ElementType);
            _mockTaxType.As<IQueryable<TaxType>>().Setup(m => m.GetEnumerator()).Returns(_queryableTaxType.GetEnumerator);

            _taxAdministrationContext.Setup(m => m.TaxType).Returns(_mockTaxType.Object);

            Assert.IsInstanceOf<Dto.DataAccess.TaxType>(_taxDataAccess.GetTaxTypeByPostalCode("7441"));
        }

        [Test]
        public void Verify_TaxAssement_Insert_When_Called()
        {
            //map classes
            _mapper = new Mapper(new MapperConfiguration(cfg =>
               cfg.CreateMap<TaxAssessment, Dto.DataAccess.TaxAssessment>().ReverseMap()
            ));
            // instantiate data access class
            _taxDataAccess = new TaxDataAccess(_taxAdministrationContext.Object, _mapper);

            _mockTaxAssessment = new Mock<DbSet<TaxAssessment>>();
            _taxAdministrationContext.Setup(m => m.TaxAssessment).Returns(_mockTaxAssessment.Object);

            Assert.IsTrue(_taxDataAccess.AddTaxAssessment(new Dto.DataAccess.TaxAssessment
                                                            {
                                                                TaxAssessmentId = 1,
                                                                NettIncome = 5000,
                                                                IncomeTax = 500,
                                                                TaxTypeId = 1,
                                                                UserCreated = "TaxAssessmentInsertTest",
                                                                DateCreated = DateTime.Now
                                                            }) > 0);
        }
    }
}
