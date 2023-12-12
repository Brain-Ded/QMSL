using NUnit.Framework;
using QMSL.Services;

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
            string valEmail = "vladyslav.atorin@gmail.com";
            string valPassword = "password";

            Assert.IsTrue(valEmail!=null && valPassword!=null);
            
        }
        [Test]
        public void LoginNeg()
        {
            string invalEmail = "";
            string invalPassword = "";
            Assert.IsTrue(invalEmail.Contains("@") && invalPassword.Length > 5);
        }
        [Test]
        public void LogoutPos()
        {
            string valEmail = "vladyslav.atorin@gmail.com";
            Assert.True(valEmail.Contains("@"));
        }
        [Test]
        public void LogoutNeg()
        {
            string invalEmail = "";
            Assert.True(invalEmail.Contains("@"));
        }
        [Test]
        public void RegisterPos()
        {
            string valEmail = "vladyslav.atorin@gmail.com";
            string valPassword = "password";
            string valName = "Vladyslav";
            string valSurname = "Atorin";
            string valFathername = "Yevhenovich";
            string valSex = "Male";
            int valAge = 20;
            string valPhone = "+380952230893";
            Assert.IsTrue(valEmail!=null && valPassword!=null && valName!=null && valSurname != null 
                && valFathername != null && valSex != null && valAge>=0 && (valPhone != null&& valPhone.StartsWith("+")));
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