using QMSL.Models;

namespace QMSL.Dtos
{
    public interface IFactory
    {
        User CreateUser(UserDto newUser);
    }
}
