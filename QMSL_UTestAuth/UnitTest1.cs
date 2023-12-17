using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MockQueryable.Moq;
using NUnit.Framework;
using QMSL;
using QMSL.Controllers;
using QMSL.Models;
using QMSL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QMSL.Dtos;

namespace QMSL_UTestAuth
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void LoginPos()
        {
            var patientTestMockDb = new Mock<DataContext>();

            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            patientTestMockDb.Setup(x => x.Patients).Returns(mock.Object);


            var mock2 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            patientTestMockDb.Setup(x => x.Doctors).Returns(mock2.Object);

            AuthController authController = new AuthController(patientTestMockDb.Object, null);

            Assert.IsNotNull(authController.Login(MockService.GetMockCredentials()));

        }
        [Test]
        public void LoginNeg()
        {
            var patientTestMockDb = new Mock<DataContext>();

            var mock = MockService.GetBadMockPatients().BuildMock().BuildMockDbSet();
            patientTestMockDb.Setup(x => x.Patients).Returns(mock.Object);


            var mock2 = MockService.GetBadMockDoctors().BuildMock().BuildMockDbSet();
            patientTestMockDb.Setup(x => x.Doctors).Returns(mock2.Object);

            AuthController authController = new AuthController(patientTestMockDb.Object, null);

            var test = authController.Login(MockService.GetMockCredentials());

            Assert.IsTrue(test.Result.Result is BadRequestObjectResult);
        }
        [Test]
        public void RegisterPos()
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Doctors).Returns(mock2.Object);

            AuthController authController = new AuthController(testMockDb.Object, null);

            UserDto userDto = new UserDto()
            {
                Name = "test",
                Surname = "test",
                Email = "test@gmail.com",
                Password = "123456789qwerty",
                Age = 18,
                Sex = "Male",
                PhoneNumber = "+1234"
            };
            var test = authController.Register(userDto, true);
            Assert.IsTrue(test.Result.Result is OkObjectResult);
        }
        [Test]
        public void RegisterNeg()
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Doctors).Returns(mock2.Object);

            AuthController authController = new AuthController(testMockDb.Object, null);

            UserDto userDto = new UserDto()
            {
                Name = "test",
                Surname = "test",
                Email = "test@gmail.com",
                Password = "123456789qwerty",
                Age = 18,
                Sex = "Male",
                PhoneNumber = "1234"
            };
            var test = authController.Register(userDto, true);
            Assert.IsTrue(test.Result.Result is BadRequestObjectResult);
        }

        [Test]
        public void TestVerificationText()
        {
            Assert.IsTrue(AuthVerifier.TextVerification("Se#x"));
        }
    }
}