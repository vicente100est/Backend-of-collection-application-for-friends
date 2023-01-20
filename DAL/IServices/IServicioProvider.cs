using Pagos.Backend.Data;

namespace Pagos.Backend.DAL.IServices
{
    public interface IServicioProvider
    {
        Task<ICollection<Servicio>> GetServicesAsync();
        Task<Servicio> GetServiceByIdAsync(int id);
    }
}
