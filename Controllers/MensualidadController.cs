using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensualidadController : ControllerBase
    {
        public GenericDTO response;
        private readonly IMensualidadProvider _monthlyPaymentQuery;

        public MensualidadController(GenericDTO response, IMensualidadProvider mensualidadProvider)
        {
            this.response = response;
            this._monthlyPaymentQuery = mensualidadProvider;
        }

        [HttpGet]
        public GenericDTO GetMonthlyPayments()
        {
            try
            {
                var lstMonthlyPayments = _monthlyPaymentQuery.GetMonthlyPayment();

                response.Success = 1;
                response.Data = lstMonthlyPayments;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("monthlyPayment/when-iduser={id}")]
        public GenericDTO GetMonthlyPaymentForUser(int id)
        {
            try
            {
                var monthlyPayment = _monthlyPaymentQuery.GetMonthlyPaymentForUser(id);

                response.Success = 1;
                response.Data = monthlyPayment;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("monthlyPayment/{id}")]
        public GenericDTO GetMonthlyPaymentById(int id)
        {
            try
            {
                var monthlyPayment = _monthlyPaymentQuery.GetMonthlyPaymentById(id);

                response.Success = 1;
                response.Data = monthlyPayment;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO AddMonthlyPayment(MensualidadEntity monthlyRequest)
        {
            try
            {
                var mpAdd = _monthlyPaymentQuery.CreateMonthlyPayment(monthlyRequest);

                if (mpAdd == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]
        public GenericDTO UpDateMonthlyPayment(int id, MensualidadEntity monthlyRequest)
        {
            try
            {
                var mpUpDate = _monthlyPaymentQuery.UpdateMonthlyPayment(id, monthlyRequest);

                if (mpUpDate == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeleteMonthlyPayment(int id)
        {
            try
            {
                var mpDelete = _monthlyPaymentQuery.DeleteMonthlyPayment(id);

                if (mpDelete == true)
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
