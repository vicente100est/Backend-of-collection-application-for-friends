using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

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

        public bool CreateService(ServicioEntity servicio)
        {
            using (var db = new DeudaContext())
            {
                Servicio serviceDb = new Servicio();
                serviceDb.NombreServicio = servicio.NombreServicio;
                serviceDb.PrecioServicio = servicio.PrecioServicio;
                db.Servicios.Add(serviceDb);
                db.SaveChanges();

                return true;
            }
        }

        public bool UpdateService(int id, ServicioEntity serviceEntity)
        {
            using (var db = new DeudaContext())
            {
                Servicio serviceDB = db.Servicios.Find(id);
                serviceDB.NombreServicio = serviceEntity.NombreServicio;
                serviceDB.PrecioServicio = serviceEntity.PrecioServicio;
                db.Entry(serviceDB).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }

        public bool DeleteService(int id)
        {
            using (var db = new DeudaContext())
            {
                Servicio serviceDB = db.Servicios.Find(id);
                db.Remove(serviceDB);
                db.SaveChanges();

                return true;
            }
        }
    }
}
