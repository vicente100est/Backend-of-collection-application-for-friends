using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Common;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pagos.Backend.DAL.Services
{
    public class AuthUsuarioProvider : IAuthUsuarioProvider
    {
        public UserDTO userResponse;
        private readonly DeudaContext _db;
        private readonly AppSettings _appSettings;

        public AuthUsuarioProvider(UserDTO response, DeudaContext db, IOptions<AppSettings> appSettings)
        {
            this.userResponse = response;
            this._db = db;
            this._appSettings = appSettings.Value;
        }

        public UserDTO AuthUser(UserAuthEntity userModel)
        {
            using (_db)
            {
                string telEncrypt = Encrypt.GetSHA256(userModel.TelefonoUsuario);

                var authUser = _db.Usuarios.Where(u => u.FechaNacimientoUsuario == userModel.FechaNacimientoUsuario
                                                && u.TelefonoUsuario == telEncrypt)
                    .FirstOrDefault();

                if (authUser == null)
                {
                    return null;
                }

                else
                {
                    userResponse.IdUsuario = authUser.IdUsuario;
                    userResponse.NombreUsuario = authUser.NombresUsuario;
                    userResponse.TokenUsuario = GetUserToken(authUser);
                }
            }

            return userResponse;
        }

        private string GetUserToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                        new Claim(ClaimTypes.MobilePhone, user.TelefonoUsuario.ToString()),
                        new Claim(ClaimTypes.DateOfBirth, user.FechaNacimientoUsuario.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenUser = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenUser);
        }
    }
}
