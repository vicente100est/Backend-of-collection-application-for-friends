using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class ServicioProvider : IServicioProvider
    {
        bool isSuccess = false;
        private readonly DeudaContext _db;
        public ServicioProvider(DeudaContext db)
        {
            this._db = db;
        }
        public Task<Servicio> GetServiceByIdAsync(int id)
        {
            using (_db)
            {
                var service = _db.Servicios.Where(s => s.IdServicio == id).FirstOrDefault();

                return Task.FromResult(service);
            }
        }

        public Task<ICollection<Servicio>> GetServicesAsync()
        {
            using (_db)
            {
                var lstService = _db.Servicios.ToList();

                return Task.FromResult((ICollection<Servicio>)lstService);
            }
        }

        public object GetServicesUser(int id)
        {
            using (_db)
            {
                var lstServicesUser =
                    from us in _db.UsuarioServicios
                    from u in _db.Usuarios
                    where us.IdServicio == id && us.IdUsuario == u.IdUsuario
                    select new
                    {
                        u.NombresUsuario,
                        u.ApellidoUsuario
                    };

                return lstServicesUser.ToList();
            }
        }

        public bool CreateService(ServicioEntity servicio)
        {
            using (_db)
            {
                Servicio serviceDb = new Servicio();
                serviceDb.NombreServicio = servicio.NombreServicio;
                serviceDb.PrecioServicio = servicio.PrecioServicio;
                _db.Servicios.Add(serviceDb);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool UpdateService(int id, ServicioEntity serviceEntity)
        {
            using (_db)
            {
                Servicio serviceDB = _db.Servicios.Find(id);
                serviceDB.NombreServicio = serviceEntity.NombreServicio;
                serviceDB.PrecioServicio = serviceEntity.PrecioServicio;
                _db.Entry(serviceDB).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteService(int id)
        {
            using (_db)
            {
                Servicio serviceDB = _db.Servicios.Find(id);
                _db.Remove(serviceDB);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
