using Pagos.Backend.DAL.IServices;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.Services
{
    public class StatusProvider : IStatusProvider
    {
        public List<StatusP> GetStatus()
        {
            using (DeudaContext db = new DeudaContext())
            {
                var lstStatus = db.StatusPs.ToList();
                return lstStatus;
            }
        }
    }
}
