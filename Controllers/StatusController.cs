using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public GenericDTO response;
        private readonly IStatusProvider _statusQuery;

        public StatusController(GenericDTO response, IStatusProvider statusProvider)
        {
            this.response = response;
            this._statusQuery = statusProvider;
        }
        [HttpGet]
        public GenericDTO Status()
        {
            try
            {
                var lstStatus = _statusQuery.GetStatus();
                response.Success = 1;
                response.Data = lstStatus;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
