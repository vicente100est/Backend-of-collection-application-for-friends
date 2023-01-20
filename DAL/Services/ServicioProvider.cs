using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;

namespace Pagos.Backend.DAL.Services
{
    public class ServicioProvider : IServicioProvider
    {
        public Task<Servicio> GetServiceByIdAsync(int id)
        {
            using (var db = new DeudaContext())
            {
                var service = db.Servicios.Where(s => s.IdServicio == id).FirstOrDefault();

                return Task.FromResult(service);
            }
        }

        public Task<ICollection<Servicio>> GetServicesAsync()
        {
            using (var db = new DeudaContext())
            {
                var lstService = db.Servicios.ToList();

                return Task.FromResult((ICollection<Servicio>)lstService);
            }
        }
    }
}
