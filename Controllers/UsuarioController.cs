using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using System.Net.WebSockets;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        public GenericDTO response;
        private readonly IUsuarioProvider _userQuery;

        public UsuarioController(GenericDTO response, IUsuarioProvider userProvider)
        {
            this.response = response;
            this._userQuery = userProvider;
        }

        [HttpGet]
        public async Task<GenericDTO> GetUserAsync()
        {
            try
            {
                var lstUsers = await _userQuery.GetUserAsync();

                response.Success = 1;
                response.Data = lstUsers;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<GenericDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userQuery.GetUserByIdAsync(id);

                response.Success = 1;
                response.Data = user;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet("userservice/user={id}")]
        public GenericDTO GetUsersService(int id)
        {
            try
            {
                var lstServices = _userQuery.GetUsersService(id);

                response.Success = 1;
                response.Data = lstServices;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public GenericDTO CreateUser(UsuarioEntity userRequest)
        {
            try
            {
                var userAdd = _userQuery.CreateUser(userRequest);

                if (userAdd == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut("{id}")]
        public GenericDTO UpdateUser(int id, UsuarioEntity userRequest)
        {
            try
            {
                var userUpDate = _userQuery.UpdateUser(id, userRequest);

                if (userUpDate == true)
                    response.Success = 1;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpDelete("{id}")]
        public GenericDTO DeleteUser(int id)
        {
            try
            {
                var userDelete = _userQuery.DeleteUser(id);

                if (userDelete == true)
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
