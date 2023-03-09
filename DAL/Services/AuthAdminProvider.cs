using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;

namespace Pagos.Backend.DAL.Services
{
    public class AuthAdminProvider : IAuthAdminProvider
    {
        public AdminDTO adminResponse;
        private readonly DeudaContext _db;

        public AuthAdminProvider(AdminDTO response, DeudaContext db)
        {
            this.adminResponse = response;
            this._db = db;
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
                }
            }

            return adminResponse;
        }
    }
}
