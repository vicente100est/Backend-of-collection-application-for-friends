using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;

namespace Pagos.Backend.DAL.Services
{
    public class UsuarioProvider : IUsuarioProvider
    {
        bool isSuccess = false;

        public bool CreateUser(UsuarioEntity usuario)
        {
            using (var db = new DeudaContext())
            {
                Usuario userDB = new Usuario();
                userDB.NombresUsuario = usuario.NombresUsuario;
                userDB.ApellidoUsuario = usuario.ApellidoUsuario;
                userDB.FechaNacimientoUsuario = usuario.FechaNacimientoUsuario;
                userDB.TelefonoUsuario = Encrypt.GetSHA256(usuario.TelefonoUsuario);
                db.Usuarios.Add(userDB);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteUser(int id)
        {
            using (var db = new DeudaContext())
            {
                Usuario userDB = db.Usuarios.Find(id);
                db.Remove(userDB);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public Task<ICollection<Usuario>> GetUserAsync()
        {
            using (var db = new DeudaContext())
            {
                var users = db.Usuarios.ToList();

                return Task.FromResult((ICollection<Usuario>)users);
            }
        }

        public Task<Usuario> GetUserByIdAsync(int id)
        {
            using (var db = new DeudaContext())
            {
                var user = db.Usuarios.Where(s => s.IdUsuario == id).FirstOrDefault();
                return Task.FromResult(user);
            }
        }

        public object GetUsersService(int id)
        {
            using (var db = new DeudaContext())
            {
                var lstUsersService =
                    from svc in db.Servicios
                    from us in db.UsuarioServicios
                    where us.IdUsuario == id && us.IdServicio == svc.IdServicio
                    select new
                    {
                        svc.NombreServicio,
                        svc.PrecioServicio
                    };

                return lstUsersService.ToList();
            }
        }

        public bool UpdateUser(int id, UsuarioEntity usuario)
        {
            using (var db = new DeudaContext())
            {
                Usuario userDB = db.Usuarios.Find(id);
                userDB.NombresUsuario = usuario.NombresUsuario;
                userDB.ApellidoUsuario = usuario.ApellidoUsuario;
                userDB.FechaNacimientoUsuario = usuario.FechaNacimientoUsuario;
                userDB.TelefonoUsuario = Encrypt.GetSHA256(usuario.TelefonoUsuario);
                db.Entry(userDB).State = EntityState.Modified;
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
