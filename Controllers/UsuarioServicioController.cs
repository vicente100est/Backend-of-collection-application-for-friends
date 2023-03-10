using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioServicioController : ControllerBase
    {
        public GenericDTO response;
        private readonly IUsuarioServicioProvider _userServiceQuery;

        public UsuarioServicioController(GenericDTO response, IUsuarioServicioProvider usuarioServicioProvider)
        {
            this.response = response;
            this._userServiceQuery = usuarioServicioProvider;
        }

        [HttpGet]
        public GenericDTO GetUserService()
        {
            try
            {
                var lstUsersServices = _userServiceQuery.GetUserService();

                response.Success = 1;
                response.Data = lstUsersServices;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public GenericDTO GetUserService(int id)
        {
            try
            {
                var userService = _userServiceQuery.GetUserServiceById(id);
                response.Success = 1;
                response.Data = userService;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO CreateUserService(UsuarioServicioEntity userServiceRequest)
        {
            try
            {
                var userServiceAdd = _userServiceQuery.AddReferUser2Service(userServiceRequest);

                if (userServiceAdd == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]
        public GenericDTO UpDateUserService(int id, UsuarioServicioEntity userServiceRequest)
        {
            try
            {
                var userServiceUpDate = _userServiceQuery.UpdateReferUser2Service(id, userServiceRequest);

                if (userServiceUpDate == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeleteUserService(int id)
        {
            try
            {
                var userServiceDelete = _userServiceQuery.DeleteReferUser2Service(id);

                if (userServiceDelete == true)
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
