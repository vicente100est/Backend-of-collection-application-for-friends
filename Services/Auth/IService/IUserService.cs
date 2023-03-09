using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Services.Auth.IService
{
    public interface IUserService
    {
        UserDTO Auth(UserAuthEntity userModel);
    }
}
