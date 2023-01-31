using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IPagosProvider
    {
        object GetPayments();
        object GetPaymentById(string idPayment);
        object GetPaymentForUser(int idUser);
        object GetPaymentForUserAndStatus(int idUser, int idStatus);
        object GetPaymentByStatus(int idStatus);
        object GetPaymentByMonthlyPayment(int monthlyPayment);
        bool CreatePayment(PagoEntity paymentEntity);
        bool UpDatePayment(string idPayment, PagoEntity paymentEntity);
        bool DeletePayment(string id);
    }
}
