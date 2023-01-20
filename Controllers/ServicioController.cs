using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        public GenericDTO response;
        private readonly IServicioProvider _servicioQuery;

        public ServicioController(GenericDTO response, IServicioProvider servicioProvider)
        {
            this.response = response;
            this._servicioQuery = servicioProvider;
        }

        [HttpGet]
        public async Task<GenericDTO> GetServicesAsync()
        {
            try
            {
                var lstService = await _servicioQuery.GetServicesAsync();

                response.Success = 1;
                response.Data = lstService;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<GenericDTO> GetServiceById(int id)
        {
            try
            {
                var service = await _servicioQuery.GetServiceByIdAsync(id);

                response.Success = 1;
                response.Data = service;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO CreateService(ServicioEntity serviceRequest)
        {
            try
            {
                var serviceAdd = _servicioQuery.CreateService(serviceRequest);

                if (serviceAdd == true)
                {
                    response.Success = 1;
                }
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]

        public GenericDTO UpdateService(int id, ServicioEntity serviceRequest)
        {
            try
            {
                var serviceUpDate = _servicioQuery.UpdateService(id, serviceRequest);

                if (serviceUpDate == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeleteService(int id)
        {
            try
            {
                var serviceDelete = _servicioQuery.DeleteService(id);

                if (serviceDelete == true)
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
