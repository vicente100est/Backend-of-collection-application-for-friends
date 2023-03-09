using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;

namespace Pagos.Backend.DAL.Services
{
    public class AuthUsuarioProvider : IAuthUsuarioProvider
    {
        public UserDTO userResponse;
        private readonly DeudaContext _db;

        public AuthUsuarioProvider(UserDTO response, DeudaContext db)
        {
            this.userResponse = response;
            this._db = db;
        }

        public UserDTO AuthUser(UserAuthEntity userModel)
        {
            using (_db)
            {
                string telEncrypt = Encrypt.GetSHA256(userModel.TelefonoUsuario);

                var authUser = _db.Usuarios.Where(u => u.FechaNacimientoUsuario == userModel.FechaNacimientoUsuario
                                                && u.TelefonoUsuario == telEncrypt)
                    .FirstOrDefault();

                userResponse.IdUsuario = authUser.IdUsuario;
                userResponse.NombreUsuario = authUser.NombresUsuario;
            }

            return userResponse;
        }
    }
}
