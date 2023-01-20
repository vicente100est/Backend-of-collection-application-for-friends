using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;

namespace Pagos.Backend.DAL.Services
{
    public class StatusProvider : IStatusProvider
    {
        public Task<ICollection<StatusP>> GetStatusAsync()
        {
            using (DeudaContext db = new DeudaContext())
            {
                var lstStatus = db.StatusPs.ToList();

                return Task.FromResult((ICollection<StatusP>)lstStatus);
            }
        }
    }
}
