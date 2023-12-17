using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MockQueryable.Moq;
using NUnit.Framework;
using QMSL;
using QMSL.Controllers;
using QMSL.Models;
using QMSL.Services;

namespace QMSL_UTestAuth
{
    public class Tests
    {
        string valEmail = "vladyslav.atorin@gmail.com";
        string valPassword = "password";
        string valName = "Vladyslav";
        string valSurname = "Atorin";
        string valFathername = "Yevhenovich";
        string valSex = "Male";
        int valAge = 20;
        string valPhone = "+380952230893";

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void LoginPos()
        {
            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            var patientTestMockDb = new Mock<DataContext>();
            patientTestMockDb.Setup(x => x.Patients).Returns(mock.Object);

            AuthController authController = new AuthController(patientTestMockDb.Object, null);

            Assert.IsNotNull(authController.Login(MockService.GetMockCredentials()));

        }
        [Test]
        public void LoginNeg()
        {
            string invalEmail = "";
            string invalPassword = "";
            Assert.IsTrue(invalEmail.Contains("@") && invalPassword.Length > 5);
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