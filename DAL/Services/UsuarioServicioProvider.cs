using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class UsuarioServicioProvider : IUsuarioServicioProvider
    {
        bool isSuccess = false;
        public object GetUserService()
        {
            using (var db = new DeudaContext())
            {
                var lstUserService =
                    from svc in db.Servicios
                    from user in db.Usuarios
                    from us in db.UsuarioServicios
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
            using (var db = new DeudaContext())
            {
                var userService =
                    from svc in db.Servicios
                    from user in db.Usuarios
                    from us in db.UsuarioServicios
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
            using (var db = new DeudaContext())
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                usuarioServicio.IdUsuario = uSE.IdUsuario;
                usuarioServicio.IdServicio = uSE.IdServicio;
                db.UsuarioServicios.Add(usuarioServicio);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteReferUser2Service(int id)
        {
            using (var db = new DeudaContext())
            {
                UsuarioServicio usuarioServicio = db.UsuarioServicios.Find(id);
                db.Remove(usuarioServicio);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
        public bool UpdateReferUser2Service(int id, UsuarioServicioEntity uSE)
        {
            using (var db = new DeudaContext())
            {
                UsuarioServicio userServiceDB = db.UsuarioServicios.Find(id);
                userServiceDB.IdUsuario = uSE.IdUsuario;
                userServiceDB.IdServicio = uSE.IdServicio;
                db.Entry(userServiceDB).State = EntityState.Modified;
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
