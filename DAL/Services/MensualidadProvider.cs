using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class MensualidadProvider : IMensualidadProvider
    {
        bool isSuccess = false;
        public bool CreateMonthlyPayment(MensualidadEntity monthlyPaymentEntity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMonthlyPayment(int id)
        {
            throw new NotImplementedException();
        }

        public object GetMonthlyPayment()
        {
            throw new NotImplementedException();
        }

        public object GetMonthlyPaymentById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMonthlyPayment(int id, MensualidadEntity monthlyPaymentEntity)
        {
            throw new NotImplementedException();
        }
    }
}
