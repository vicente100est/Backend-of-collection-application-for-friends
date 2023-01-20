using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IServicioProvider
    {
        Task<ICollection<Servicio>> GetServicesAsync();
        Task<Servicio> GetServiceByIdAsync(int id);
        bool CreateService(ServicioEntity servicio);
        bool UpdateService(int id, ServicioEntity serviceEntity);
        bool DeleteService(int id);
    }
}
