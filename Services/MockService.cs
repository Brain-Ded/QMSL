using QMSL.Dtos;
using QMSL.Models;

namespace QMSL.Services
{
    public static class MockService
    {
        public static List<Patient> GetMockPatients()
        {
            return new List<Patient>()
            {
                new Patient()
                {
                    Id = 1,
                    Name = "Foo",
                    Surname = "Bar",
                    Fathername = "Rab",
                    Sex = "Male",
                    Age = 18,
                    Disease = "Cancer",
                    Email = "patient@gmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "+3805452432",
                },
                new Patient()
                {
                    Id = 1,
                    Name = "Foor",
                    Surname = "Barb",
                    Fathername = "Rabe",
                    Sex = "Male",
                    Age = 21,
                    Disease = "Cancer",
                    Email = "patient2@gmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "+380532447732",
                    
                },
            };
        }

        public static Patient GetMockPatient()
        {
            return new Patient()
            {
                Id = 3,
                Name = "Foo",
                Surname = "Bar",
                Fathername = "Rab",
                Sex = "Male",
                Age = 18,
                Disease = "Cancer",
                Email = "patientMock@gmail.com",
                Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                PhoneNumber = "+3805452432",
                Doctors = new List<Doctor> 
                { 
                    new Doctor()
                    {
                        Id = 1,
                        Name = "Foo",
                        Surname = "Bar",
                        Fathername = "Rab",
                        Sex = "Male",
                        Age = 18,
                        Email = "patientMock@gmail.com",
                        Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                        PhoneNumber = "+3805452432",
                    }
                },
            };
        }

        public static Credentials GetMockCredentials() 
        {
            return new Credentials()
            {
                Email = "patient@gmail.com",
                Password = "string",
            };
        }
    }
}
