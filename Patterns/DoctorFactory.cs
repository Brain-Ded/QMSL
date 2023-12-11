using Azure.Core;
using Microsoft.AspNetCore.Identity;
using QMSL.Controllers;
using QMSL.Models;
using System.Security.Cryptography;

namespace QMSL.Dtos
{
    public class DoctorFactory : IFactory
    {
        public User CreateUser(UserDto newUser)
        {
            return new Doctor()
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Fathername = newUser.Fathername,
                PhoneNumber = newUser.PhoneNumber,
                Sex = newUser.Sex,
                Age = newUser.Age,
                Email = newUser.Email
            };
        }
    }
}
