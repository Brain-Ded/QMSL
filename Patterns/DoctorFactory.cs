using QMSL.Models;

namespace QMSL.Dtos
{
    public class DoctorFactory : IFactory
    {
        public User CreateUser()
        {
            return new Doctor();
        }
    }
}
