using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DTO;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<GenericDTO> StatusAsync()
        {
            try
            {
                var lstStatus = await _statusQuery.GetStatusAsync();
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
