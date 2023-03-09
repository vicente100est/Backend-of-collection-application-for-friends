using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IAuthAdminProvider
    {
        AdminDTO AuthAdmin(AdminAuthEntity adminModel);
    }
}
