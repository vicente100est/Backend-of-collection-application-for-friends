using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Common;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Pagos.Backend.DAL.Services
{
    public class AuthAdminProvider : IAuthAdminProvider
    {
        public AdminDTO adminResponse;
        private readonly DeudaContext _db;
        private readonly AppSettings _appSettings;

        public AuthAdminProvider(AdminDTO response, DeudaContext db, IOptions<AppSettings> appSettings)
        {
            this.adminResponse = response;
            this._db = db;
            this._appSettings = appSettings.Value;
        }

        public AdminDTO AuthAdmin(AdminAuthEntity adminModel)
        {
            using (_db)
            {
                string passEncrypt = Encrypt.GetSHA256(adminModel.ContrasenaAdministrador);

                var authAdmin = _db.Administradors.Where(a => a.NombreAdministrador == adminModel.NombreAdministrador
                                                        && a.ContrasenaAdministrador == passEncrypt)
                    .FirstOrDefault();

                if (authAdmin == null)
                {
                    return null;
                }
                else
                {
                    adminResponse.NombreAdministrador = authAdmin.NombreAdministrador;
                    adminResponse.TokenAdministrador = GetAdminToken(authAdmin);
                }
            }

            return adminResponse;
        }

        private string GetAdminToken(Administrador admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key =  Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, admin.IdAdministrador.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenAdmin = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenAdmin);
        }
    }
}
