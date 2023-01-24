using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                using (var db = new DeudaContext())
                {
                    var lstUserService =
                        from svc in db.Servicios
                        from user in db.Usuarios
                        from us in db.UsuarioServicios
                        where us.IdUsuario == user.IdUsuario && us.IdServicio == svc.IdServicio
                        select new
                        {
                            us.IdUs,
                            user.NombresUsuario,
                            svc.NombreServicio
                        };

                    response.Success = 1;
                    response.Data = lstUserService.ToList();
                }
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
                using (var db = new DeudaContext())
                {
                    var userService =
                        from svc in db.Servicios
                        from user in db.Usuarios
                        from us in db.UsuarioServicios
                        where us.IdUsuario == user.IdUsuario
                            && us.IdServicio == svc.IdServicio
                            && us.IdUs == id
                        select new
                        {
                            us.IdUs,
                            user.NombresUsuario,
                            svc.NombreServicio
                        };

                    response.Success = 1;
                    response.Data = userService.FirstOrDefault();
                }
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
