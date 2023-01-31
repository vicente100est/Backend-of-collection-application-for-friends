using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;

namespace Pagos.Backend.DAL.Services
{
    public class StatusProvider : IStatusProvider
    {
        private readonly DeudaContext _db;
        public StatusProvider(DeudaContext db)
        {
            this._db = db;
        }
        public Task<ICollection<StatusP>> GetStatusAsync()
        {
            using (_db)
            {
                var lstStatus = _db.StatusPs.ToList();

                return Task.FromResult((ICollection<StatusP>)lstStatus);
            }
        }
    }
}
