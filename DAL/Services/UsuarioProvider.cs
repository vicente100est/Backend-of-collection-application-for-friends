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
        private readonly DeudaContext _db;
        public UsuarioProvider(DeudaContext db)
        {
            this._db = db;
        }

        public bool CreateUser(UsuarioEntity usuario)
        {
            using (_db)
            {
                Usuario userDB = new Usuario();
                userDB.NombresUsuario = usuario.NombresUsuario;
                userDB.ApellidoUsuario = usuario.ApellidoUsuario;
                userDB.FechaNacimientoUsuario = usuario.FechaNacimientoUsuario;
                userDB.TelefonoUsuario = Encrypt.GetSHA256(usuario.TelefonoUsuario);
                _db.Usuarios.Add(userDB);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteUser(int id)
        {
            using (_db)
            {
                Usuario userDB = _db.Usuarios.Find(id);
                _db.Remove(userDB);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public Task<ICollection<Usuario>> GetUserAsync()
        {
            using (_db)
            {
                var users = _db.Usuarios.ToList();

                return Task.FromResult((ICollection<Usuario>)users);
            }
        }

        public Task<Usuario> GetUserByIdAsync(int id)
        {
            using (_db)
            {
                var user = _db.Usuarios.Where(s => s.IdUsuario == id).FirstOrDefault();
                return Task.FromResult(user);
            }
        }

        public object GetUsersService(int idusuario)
        {
            using (_db)
            {
                var lstUsersService =
                    from svc in _db.Servicios
                    from us in _db.UsuarioServicios
                    where us.IdUsuario == idusuario && us.IdServicio == svc.IdServicio
                    select new
                    {
                        ServicioId = svc.IdServicio,
                        ServicioName = svc.NombreServicio,
                        ServicioPriceU = svc.PrecioServicio/(from us in _db.UsuarioServicios
                                                            where us.IdServicio == svc.IdServicio
                                                            select us).Count()
                    };

                return lstUsersService.ToList();
            }
        }

        public bool UpdateUser(int id, UsuarioEntity usuario)
        {
            using (_db)
            {
                Usuario userDB = _db.Usuarios.Find(id);
                userDB.NombresUsuario = usuario.NombresUsuario;
                userDB.ApellidoUsuario = usuario.ApellidoUsuario;
                userDB.FechaNacimientoUsuario = usuario.FechaNacimientoUsuario;
                userDB.TelefonoUsuario = Encrypt.GetSHA256(usuario.TelefonoUsuario);
                _db.Entry(userDB).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
