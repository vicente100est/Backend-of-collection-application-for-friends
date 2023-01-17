using Pagos.Backend.Data;

namespace Pagos.Backend.DAL.IServices
{
    public interface IStatusProvider
    {
        List<StatusP> GetStatus();
    }
}
