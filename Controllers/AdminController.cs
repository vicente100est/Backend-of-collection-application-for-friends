using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public GenericDTO response;
        private readonly IAdminProvider _adminQuery;

        public AdminController(GenericDTO response, IAdminProvider adminProvider)
        {
            this.response = response;
            this._adminQuery = adminProvider;
        }

        [HttpGet]
        public async Task<GenericDTO> AdminAsync()
        {
            try
            {
                var lstAdmins = await _adminQuery.GetAdminsAsync();
                response.Success = 1;
                response.Data = lstAdmins;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO CreateAdmin(AdministradorEntity adminRequest)
        {
            try
            {
                var adminAdd = _adminQuery.CreateAdmin(adminRequest);

                if (adminAdd == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]
        public GenericDTO UpdateAdmin(int id, AdministradorEntity adminRequest)
        {
            try
            {
                var adminUpdate = _adminQuery.UpdateAdmin(id, adminRequest);

                if (adminUpdate == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeleteAdmin(int id)
        {
            try
            {
                var adminDelete = _adminQuery.DeleteAdmin(id);

                if (adminDelete == true)
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
