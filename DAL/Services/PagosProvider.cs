using Microsoft.EntityFrameworkCore;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class PagosProvider : IPagosProvider
    {
        bool isSuccess = false;
        private readonly DeudaContext _db;
        public PagosProvider(DeudaContext db)
        {
            this._db = db;
        }
        public bool CreatePayment(PagoEntity paymentEntity)
        {
            using (_db)
            {
                Pago payment = new Pago();
                payment.IdPago = Guid.NewGuid().ToString();
                payment.PrecioPago = paymentEntity.PrecioPago;
                payment.IdStatus = paymentEntity.IdStatus;
                payment.IdMensualidad = paymentEntity.IdMensualidad;
                payment.IdUsuario = paymentEntity.IdUsuario;

                _db.Pagos.Add(payment);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public bool DeletePayment(string id)
        {
            using (_db)
            {
                Pago pagoDb = _db.Pagos.Find(id);

                _db.Remove(pagoDb);
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }

        public object GetPaymentById(string idPayment)
        {
            using (_db)
            {
                var paymentById =
                    (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
                     from usr in _db.Usuarios
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
            using (_db)
            {
                var lstPaymentByMonth =
                    (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
                     from usr in _db.Usuarios
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
            using (_db)
            {
                var lstPaymetsByStatus =
                    (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
                     from usr in _db.Usuarios
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
            using (_db)
            {
                var lstPaymentsUser =
                    (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
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

        public object GetPaymentForUserAndStatus(int idUser, int idStatus)
        {
            using (_db)
            {
                var lstPaymentUserAndStatus =
                     (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
                     where sts.IdStatus == idStatus &&
                         pay.IdStatus == sts.IdStatus &&
                         pay.IdMensualidad == mon.IdMensualidad &&
                         pay.IdUsuario == idUser

                     select new
                     {
                         pay.IdPago,
                         pay.PrecioPago,
                         sts.NombreStatus,
                         mon.NombreMensualidad
                     }).ToList();

                return lstPaymentUserAndStatus;
            }
        }
        
        public object GetPayments()
        {
            using (_db)
            {
                var lstPayments =
                    (from pay in _db.Pagos
                     from sts in _db.StatusPs
                     from mon in _db.Mensualidads
                     from usr in _db.Usuarios
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
            using (_db)
            {
                Pago paymentDb = _db.Pagos.Find(idPayment);
                paymentDb.PrecioPago = paymentEntity.PrecioPago;
                paymentDb.IdStatus = paymentEntity.IdStatus;
                paymentDb.IdMensualidad = paymentEntity.IdMensualidad;
                paymentEntity.IdUsuario = paymentEntity.IdUsuario;

                _db.Entry(paymentDb).State = EntityState.Modified;
                _db.SaveChanges();

                isSuccess = true;

                return isSuccess;
            }
        }
    }
}
