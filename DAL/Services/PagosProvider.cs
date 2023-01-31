using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class PagosProvider : IPagosProvider
    {
        bool isSuccess = false;
        public bool CreatePayment(PagoEntity paymentEntity)
        {
            using (var db = new DeudaContext())
            {
                Pago payment = new Pago();
                payment.IdPago = Guid.NewGuid().ToString();
                payment.PrecioPago = paymentEntity.PrecioPago;
                payment.IdStatus = paymentEntity.IdStatus;
                payment.IdMensualidad = paymentEntity.IdMensualidad;
                payment.IdUsuario = paymentEntity.IdUsuario;

                db.Pagos.Add(payment);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeletePayment(string id)
        {
            using (var db = new DeudaContext())
            {
                Pago pagoDb = db.Pagos.Find(id);

                db.Remove(pagoDb);
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public object GetPaymentById(string idPayment)
        {
            using (var db = new DeudaContext())
            {
                var paymentById =
                    (from pay in db.Pagos
                     from sts in db.StatusPs
                     from mon in db.Mensualidads
                     from usr in db.Usuarios
                     where pay.IdPago == idPayment &&
                         pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == usr.IdUsuario

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad,
                         usr.NombresUsuario,
                         usr.ApellidoUsuario
                     }).ToList();

                return paymentById;
            }
        }

        public object GetPaymentByMonthlyPayment(int monthlyPayment)
        {
            using (var db = new DeudaContext())
            {
                var lstPaymentByMonth =
                    (from pay in db.Pagos
                     from sts in db.StatusPs
                     from mon in db.Mensualidads
                     from usr in db.Usuarios
                     where pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == monthlyPayment &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == usr.IdUsuario

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad,
                         usr.NombresUsuario,
                         usr.ApellidoUsuario
                     }).ToList();

                return lstPaymentByMonth;
            }
        }

        public object GetPaymentByStatus(int idStatus)
        {
            using (var db = new DeudaContext())
            {
                var lstPaymetsByStatus =
                    (from pay in db.Pagos
                     from sts in db.StatusPs
                     from mon in db.Mensualidads
                     from usr in db.Usuarios
                     where pay.IdStatus == idStatus &&
                         pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == usr.IdUsuario

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad,
                         usr.NombresUsuario,
                         usr.ApellidoUsuario
                     }).ToList();

                return lstPaymetsByStatus;
            }
        }

        public object GetPaymentForUser(int idUser)
        {
            using (var db = new DeudaContext())
            {
                var lstPaymentsUser =
                    (from pay in db.Pagos
                     from sts in db.StatusPs
                     from mon in db.Mensualidads
                     where pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == idUser

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad
                     }).ToList();

                return lstPaymentsUser;
            }
        }

        public object GetPayments()
        {
            using (var db = new DeudaContext())
            {
                var lstPayments =
                    (from pay in db.Pagos
                     from sts in db.StatusPs
                     from mon in db.Mensualidads
                     from usr in db.Usuarios
                     where pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == usr.IdUsuario

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad,
                         usr.NombresUsuario,
                         usr.ApellidoUsuario
                     }).ToList();

                return lstPayments;
            }
        }

        public bool UpDatePayment(string idPayment, PagoEntity paymentEntity)
        {
            using (var db = new DeudaContext())
            {
                Pago paymentDb = db.Pagos.Find(idPayment);
                paymentDb.PrecioPago = paymentEntity.PrecioPago;
                paymentDb.IdStatus = paymentEntity.IdStatus;
                paymentDb.IdMensualidad = paymentEntity.IdMensualidad;
                paymentEntity.IdUsuario = paymentEntity.IdUsuario;

                db.Entry(paymentDb).State = EntityState.Modified;
                db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
