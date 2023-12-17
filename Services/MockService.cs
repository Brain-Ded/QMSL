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
        public static List<Patient> GetBadMockPatients()
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
                    Email = "patientgmail.com",
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
                    PhoneNumber = "380532447732",

                },
            };
        }
        public static List<Doctor> GetMockDoctors()
        {
            return new List<Doctor>()
            {
                new Doctor()
                {
                    Id = 1,
                    Name = "Foo",
                    Surname = "Bar",
                    Fathername = "Rab",
                    Sex = "Male",
                    Age = 18,
                    Email = "doctor@gmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "+3805452432",
                },
                new Doctor()
                {
                    Id = 1,
                    Name = "Foor",
                    Surname = "Barb",
                    Fathername = "Rabe",
                    Sex = "Male",
                    Age = 21,
                    Email = "doctor2@gmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "+380532447732",

                },
            };
        }
        public static List<Doctor> GetBadMockDoctors()
        {
            return new List<Doctor>()
            {
                new Doctor()
                {
                    Id = 1,
                    Name = "Foo",
                    Surname = "Bar",
                    Fathername = "Rab",
                    Sex = "Male",
                    Age = 18,
                    Email = "doctorgmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "+3805452432",
                },
                new Doctor()
                {
                    Id = 1,
                    Name = "Foor",
                    Surname = "Barb",
                    Fathername = "Rabe",
                    Sex = "Male",
                    Age = 21,
                    Email = "doctor2@gmail.com",
                    Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                    PhoneNumber = "380532447732",

                },
            };
        }
        public static Patient GetMockPatient()
        {
            var patient = new Patient()
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
                
            };

            patient.Doctors.Add(new Doctor()
            {
                Id = 1,
                Name = "Foor",
                Surname = "Barb",
                Fathername = "Rabe",
                Sex = "Male",
                Age = 21,
                Email = "patient2@gmail.com",
                Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                PhoneNumber = "+380532447732",
            });

            return patient;
        }
        public static Doctor GetMockDoctor()
        {
            var doctor = new Doctor()
            {
                Id = 1,
                Name = "Foor",
                Surname = "Barb",
                Fathername = "Rabe",
                Sex = "Male",
                Age = 21,
                Email = "patient2@gmail.com",
                Password = Convert.FromBase64String("gweijPaRKick7k1VbG0zxrUr5NX92ImLokyp4WD1Yp2r2pZfLUINTjVdBIs8lDt/uS2jXBvnQdikhB3aNcYApg=="),
                PhoneNumber = "+380532447732",
            };

            doctor.Patients.Add(new Patient()
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
            });

            return doctor;
        }
        public static Credentials GetMockCredentials() 
        {
            return new Credentials()
            {
                Email = "patient@gmail.com",
                Password = "string",
            };
        }
        public static List<EditablePoll> GetEditablePolls()
        {
            return new List<EditablePoll>()
            {
                new EditablePoll()
                {
                    Id = 1,
                    Name = "test",
                    Questions = new List<EditableQuestion>(),
                    AssignedAt = DateTime.Now
                },
                new EditablePoll()
                {
                    Id = 2,
                    Name = "test1",
                    Questions = new List<EditableQuestion>(),
                    AssignedAt = DateTime.Now
                },
            };
        }
        public static List<GeneralPoll> GetGeneralPolls()
        {
            return new List<GeneralPoll>()
            {
                new GeneralPoll()
                {
                    Id = 1,
                    Name = "test",
                    Questions = new List<GeneralQuestion>(),
                },
                new GeneralPoll()
                {
                    Id = 2,
                    Name = "test1",
                    Questions = new List<GeneralQuestion>()
                },
            };
        }
    }
}
