using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class UsuarioServicioProvider : IUsuarioServicioProvider
    {
        private readonly DeudaContext _db;
        public bool isSuccess = false;
        public UsuarioServicioProvider(DeudaContext db)
        {
            this._db = db;
        }

        public object GetUserService()
        {
            using (_db)
            {
                var lstUserService =
                    from svc in _db.Servicios
                    from user in _db.Usuarios
                    from us in _db.UsuarioServicios
                    where us.IdUsuario == user.IdUsuario && us.IdServicio == svc.IdServicio
                    select new
                    {
                        us.IdUs,
                        user.NombresUsuario,
                        svc.NombreServicio
                    };

                return lstUserService.ToList();
            }
        }

        public object GetUserServiceById(int id)
        {
            using (_db)
            {
                var userService =
                    from svc in _db.Servicios
                    from user in _db.Usuarios
                    from us in _db.UsuarioServicios
                    where us.IdUsuario == user.IdUsuario
                        && us.IdServicio == svc.IdServicio
                        && us.IdUs == id
                    select new
                    {
                        us.IdUs,
                        user.NombresUsuario,
                        svc.NombreServicio
                    };

                return userService.FirstOrDefault();
            }
        }
        public bool AddReferUser2Service(UsuarioServicioEntity uSE)
        {
            using (_db)
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                usuarioServicio.IdUsuario = uSE.IdUsuario;
                usuarioServicio.IdServicio = uSE.IdServicio;
                _db.UsuarioServicios.Add(usuarioServicio);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteReferUser2Service(int id)
        {
            using (_db)
            {
                UsuarioServicio usuarioServicio = _db.UsuarioServicios.Find(id);
                _db.Remove(usuarioServicio);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
        public bool UpdateReferUser2Service(int id, UsuarioServicioEntity uSE)
        {
            using (_db)
            {
                UsuarioServicio userServiceDB = _db.UsuarioServicios.Find(id);
                userServiceDB.IdUsuario = uSE.IdUsuario;
                userServiceDB.IdServicio = uSE.IdServicio;
                _db.Entry(userServiceDB).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
