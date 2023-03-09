using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Services.Auth.IService;

namespace Pagos.Backend.Services.Auth.Service
{
    public class AdminService : IAdminService
    {
        public AdminDTO adminResponse;
        private readonly IAuthAdminProvider _authAdminQuery;

        public AdminService(AdminDTO response, IAuthAdminProvider authAdminProvider)
        {
            this.adminResponse = response;
            this._authAdminQuery = authAdminProvider;
        }

        public AdminDTO Auth(AdminAuthEntity adminModel)
        {
            var adminData = _authAdminQuery.AuthAdmin(adminModel);

            return adminData;
        }
    }
}
