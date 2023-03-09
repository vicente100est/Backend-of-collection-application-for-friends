using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Pagos.Backend.DAL.Services
{
    public class AdminProvider : IAdminProvider
    {
        public bool isSuccess = false;
        private readonly DeudaContext _db;

        public AdminProvider(DeudaContext db)
        {
            this._db = db;
        }

        public bool CreateAdmin(AdministradorEntity admin)
        {
            using (_db)
            {
                Administrador adminDb = new Administrador();
                adminDb.NombreAdministrador = admin.NombreAdministrador;
                adminDb.ContrasenaAdministrador = Encrypt.GetSHA256(admin.ContrasenaAdministrador);
                _db.Administradors.Add(adminDb);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteAdmin(int id)
        {
            using (_db)
            {
                Administrador adminDb = _db.Administradors.Find(id);
                _db.Remove(adminDb);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public Task<ICollection<Administrador>> GetAdminsAsync()
        {
            using (_db)
            {
                var lstAdmins = _db.Administradors.ToList();

                return Task.FromResult((ICollection<Administrador>)lstAdmins);
            }
        }

        public bool UpdateAdmin(int id, AdministradorEntity admin)
        {
            using (_db)
            {
                Administrador adminDb = _db.Administradors.Find(id);
                adminDb.NombreAdministrador = admin.NombreAdministrador;
                adminDb.ContrasenaAdministrador = Encrypt.GetSHA256(admin.ContrasenaAdministrador);
                _db.Entry(adminDb).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
