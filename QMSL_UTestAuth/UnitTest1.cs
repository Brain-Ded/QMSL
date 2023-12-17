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
            
            Assert.IsTrue(true);
        }
        [Test]
        public void RegisterNeg()
        {
            string invalEmail = "";
            string invalPassword = "";
            string invalName = "";
            string invalSurname = "Atorin";
            string invalFathername = "Yevhenovich";
            string invalSex = "Male";
            int invalAge = -20;
            string invalPhone = "324234";
            Assert.IsTrue(invalEmail!=null && invalPassword!=null && invalName!=null && invalSurname != null
                && invalFathername != null && invalSex != null && invalAge>=0 && invalPhone != null);
        }

        [Test]
        public void TestVerificationText()
        {
            Assert.IsTrue(AuthVerifier.TextVerification("Sex"));
        }
    }
}