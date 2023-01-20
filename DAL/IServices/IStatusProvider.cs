using Pagos.Backend.Data;
using Pagos.Backend.DTO;

namespace Pagos.Backend.DAL.IServices
{
    public interface IStatusProvider
    {
        Task<ICollection<StatusP>> GetStatusAsync();
    }
}
