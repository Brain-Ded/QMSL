using QMSL.Models;

namespace QMSL.Dtos
{
    public class PatientFactory : IFactory
    {
        public User CreateUser(UserDto newUser)
        {
            return new Patient()
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Fathername = newUser.Fathername,
                PhoneNumber = newUser.PhoneNumber,
                Sex = newUser.Sex,
                Age = newUser.Age,
                Email = newUser.Email,
                Disease = newUser.Disease              
            };
        }
    }
}
