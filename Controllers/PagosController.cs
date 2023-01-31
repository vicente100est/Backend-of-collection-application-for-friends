using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        public GenericDTO response;
        private readonly IPagosProvider _paymentsQuery;
        public PagosController(GenericDTO response, IPagosProvider pagosProvider)
        {
            this.response = response;
            this._paymentsQuery = pagosProvider;
        }

        [HttpGet]
        public GenericDTO GetPayments()
        {
            try
            {
                var lstPayments = _paymentsQuery.GetPayments();

                response.Success = 1;
                response.Data = lstPayments;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public GenericDTO GetPaymentById(string id)
        {
            try
            {
                var payment = _paymentsQuery.GetPaymentById(id);

                response.Success = 1;
                response.Data = payment;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("payment/when-user={id}")]
        public GenericDTO GetPaymentByUser(int id)
        {
            try
            {
                var lstPaymentsByUser = _paymentsQuery.GetPaymentForUser(id);

                response.Success = 1;
                response.Data = lstPaymentsByUser;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("payment/when-status={id}")]
        public GenericDTO GetPaymentByStatus(int id)
        {
            try
            {
                var lstPaymentByStatus = _paymentsQuery.GetPaymentByStatus(id);

                response.Success = 1;
                response.Data = lstPaymentByStatus;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("payment/when-monthlypayment={id}")]
        public GenericDTO GetPaymentByMonthlyPayment(int id)
        {
            try
            {
                var lstPaymentByMonthlyPayment = _paymentsQuery.GetPaymentByMonthlyPayment(id);

                response.Success = 1;
                response.Data = lstPaymentByMonthlyPayment;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO AddPayment(PagoEntity paymentRequest)
        {
            try
            {
                var paymentAdd = _paymentsQuery.CreatePayment(paymentRequest);

                if (paymentAdd == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]
        public GenericDTO UpDatePayment(string id, PagoEntity paymentRequest)
        {
            try
            {
                var paymentAlter = _paymentsQuery.UpDatePayment(id, paymentRequest);

                if (paymentAlter == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeletePayment(string id)
        {
            try
            {
                var paymentDelete = _paymentsQuery.DeletePayment(id);

                if (paymentDelete == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
