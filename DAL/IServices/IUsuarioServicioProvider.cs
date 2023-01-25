using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IUsuarioServicioProvider
    {
        object GetUserService();
        object GetUserServiceById(int id);
        bool AddReferUser2Service(UsuarioServicioEntity uSE);
        bool UpdateReferUser2Service(int id, UsuarioServicioEntity uSE);
        bool DeleteReferUser2Service(int id);
    }
}
