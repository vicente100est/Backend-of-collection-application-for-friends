using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class MensualidadProvider : IMensualidadProvider
    {
        bool isSuccess = false;
        private readonly DeudaContext _db;
        public MensualidadProvider(DeudaContext db)
        {
            this._db = db;
        }
        public bool CreateMonthlyPayment(MensualidadEntity monthlyPaymentEntity)
        {
            using (_db)
            {
                Mensualidad monthDB = new Mensualidad();
                monthDB.NombreMensualidad = monthlyPaymentEntity.NombreMensualidad;
                monthDB.PrecioMensualidad = monthlyPaymentEntity.PrecioMensualidad;
                monthDB.IdServicio = monthlyPaymentEntity.IdServicio;

                _db.Mensualidads.Add(monthDB);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteMonthlyPayment(int id)
        {
            using (_db)
            {
                Mensualidad monthDB = _db.Mensualidads.Find(id);

                _db.Remove(monthDB);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public object GetMonthlyPayment()
        {
            using (_db)
            {
                var monthlyPayment =
                    (from svc in _db.Servicios
                     from month in _db.Mensualidads
                     where svc.IdServicio == month.IdServicio
                     select new
                     {
                         month.IdMensualidad,
                         month.NombreMensualidad,
                         month.PrecioMensualidad,
                         svc.NombreServicio
                     }).ToList();

                return monthlyPayment;
            }
        }
        public object GetMonthlyPaymentForUser(int id)
        {
            using (_db)
            {
                var monthlyPayment =
                    (from svc in _db.Servicios
                     from us in _db.UsuarioServicios
                     from month in _db.Mensualidads
                     where svc.IdServicio == month.IdServicio && us.IdUsuario == id
                     select new
                     {
                         month.IdMensualidad,
                         month.NombreMensualidad,
                         month.PrecioMensualidad,
                         svc.NombreServicio
                     }).ToList();

                return monthlyPayment;
            }
        }

        public object GetMonthlyPaymentById(int id)
        {
            using (_db)
            {
                var monthlyPaymentById =
                    (from svc in _db.Servicios
                     from month in _db.Mensualidads
                     where svc.IdServicio == month.IdServicio && month.IdMensualidad == id
                     select new
                     {
                         month.IdMensualidad,
                         month.NombreMensualidad,
                         month.PrecioMensualidad,
                         svc.NombreServicio
                     }).FirstOrDefault();

                return monthlyPaymentById;
            }
        }

        public bool UpdateMonthlyPayment(int id, MensualidadEntity monthlyPaymentEntity)
        {
            using (_db)
            {
                Mensualidad monthDB = _db.Mensualidads.Find(id);
                monthDB.NombreMensualidad = monthlyPaymentEntity.NombreMensualidad;
                monthlyPaymentEntity.IdServicio = monthlyPaymentEntity.IdServicio;

                _db.Entry(monthDB).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
