using QMSL.Models;

namespace QMSL.Dtos
{
    public class PatientFactory : IFactory
    {
        public User CreateUser()
        {
            return new Patient();
        }
    }
}
