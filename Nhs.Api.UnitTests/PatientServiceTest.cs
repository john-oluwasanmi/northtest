using AutoMapper;
using Moq;
using Nhs.Test.Api.BusinessService;
using Nhs.Test.Api.Model;


namespace Avocado.Religion.Api.BusinessServices.Tests
{
    public class PatientServiceTest
    {
        protected Mock<IMapper> _mapperMocked;
        private Mock<IPatientRepository> _patientRepositoryMocked;
        private Mock<ICacheService> _cacheServiceMocked;
        PatientService _patientService;


        [SetUp]
        public void Setup()
        {
            _mapperMocked = new Mock<IMapper>();
            _patientRepositoryMocked = new Mock<IPatientRepository>();
            _cacheServiceMocked = new Mock<ICacheService>();

            _patientService = new PatientService(_patientRepositoryMocked.Object, _cacheServiceMocked.Object, _mapperMocked.Object);
        }


        [Test]

        public void When_Get_Patient_and_entity_not_found_in_cache_then_set_cache_and_return_item()
        {

            //arrange
            Patient actual = new Patient { Id = 1 };
            PatientDTO expected = new PatientDTO { Id = 1 };

            _patientRepositoryMocked.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(actual);
            _cacheServiceMocked.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(null as Patient);
            _mapperMocked.Setup(m => m.Map<PatientDTO>(actual)).Returns(expected);
            _cacheServiceMocked.Setup(r => r.SetAsync(actual));

            //act
            var data = _patientService.GetByIdAsync(expected.Id).Result;


            //assert
            Assert.That(actual.Id, Is.EqualTo(expected.Id));

        }


        [Test]

        public void When_Get_Patient_and_entity_found_in_cache_then_return_item()
        {


            //arrange
            Patient actual = new Patient { Id = 1 };
            PatientDTO expected = new PatientDTO { Id = 1 };

            _patientRepositoryMocked.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(null as Patient);
            _cacheServiceMocked.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(actual);
            _mapperMocked.Setup(m => m.Map<PatientDTO>(actual)).Returns(expected);
            _cacheServiceMocked.Setup(r => r.SetAsync(actual));

            //act
            var data = _patientService.GetByIdAsync(expected.Id).Result;


            //assert
            Assert.That(actual.Id, Is.EqualTo(expected.Id));

        }
    }
}
