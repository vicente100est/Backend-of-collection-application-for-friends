using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IMensualidadProvider
    {
        object GetMonthlyPayment();
        object GetMonthlyPaymentForUser(int id);
        object GetMonthlyPaymentById(int id);
        bool CreateMonthlyPayment(MensualidadEntity monthlyPaymentEntity);
        bool UpdateMonthlyPayment(int id, MensualidadEntity monthlyPaymentEntity);
        bool DeleteMonthlyPayment(int id);
    }
}
