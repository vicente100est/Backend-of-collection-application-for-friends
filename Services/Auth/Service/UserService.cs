using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Services.Auth.IService;

namespace Pagos.Backend.Services.Auth.Service
{
    public class UserService : IUserService
    {
        public UserDTO userResponse;
        private readonly IAuthUsuarioProvider _authUserQuery;

        public UserService(UserDTO response, IAuthUsuarioProvider usuarioProvider)
        {
            this.userResponse = response;
            this._authUserQuery = usuarioProvider;
        }
        public UserDTO Auth(UserAuthEntity userModel)
        {
            var userData = _authUserQuery.AuthUser(userModel);

            return userData;
        }
    }
}
