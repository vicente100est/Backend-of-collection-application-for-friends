using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IAdminProvider
    {
        Task<ICollection<Administrador>> GetAdminsAsync();
        bool CreateAdmin(AdministradorEntity admin);
        bool UpdateAdmin(int id, AdministradorEntity admin);
        bool DeleteAdmin(int id);
    }
}
