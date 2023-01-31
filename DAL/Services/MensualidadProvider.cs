using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class MensualidadProvider : IMensualidadProvider
    {
        bool isSuccess = false;
        public bool CreateMonthlyPayment(MensualidadEntity monthlyPaymentEntity)
        {
            using (var db = new DeudaContext())
            {
                Mensualidad monthDB = new Mensualidad();
                monthDB.NombreMensualidad = monthlyPaymentEntity.NombreMensualidad;
                monthDB.PrecioMensualidad = monthlyPaymentEntity.PrecioMensualidad;
                monthDB.IdServicio = monthlyPaymentEntity.IdServicio;

                db.Mensualidads.Add(monthDB);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeleteMonthlyPayment(int id)
        {
            using (var db = new DeudaContext())
            {
                Mensualidad monthDB = db.Mensualidads.Find(id);

                db.Remove(monthDB);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public object GetMonthlyPayment()
        {
            using (var db = new DeudaContext())
            {
                var monthlyPayment =
                    (from svc in db.Servicios
                     from month in db.Mensualidads
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
            using (var db = new DeudaContext())
            {
                var monthlyPayment =
                    (from svc in db.Servicios
                     from us in db.UsuarioServicios
                     from month in db.Mensualidads
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
            using (var db = new DeudaContext())
            {
                var monthlyPaymentById =
                    (from svc in db.Servicios
                     from month in db.Mensualidads
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
            using (var db = new DeudaContext())
            {
                Mensualidad monthDB = db.Mensualidads.Find(id);
                monthDB.NombreMensualidad = monthlyPaymentEntity.NombreMensualidad;
                monthlyPaymentEntity.IdServicio = monthlyPaymentEntity.IdServicio;

                db.Entry(monthDB).State = EntityState.Modified;
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
