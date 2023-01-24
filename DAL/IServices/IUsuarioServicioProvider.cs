using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IUsuarioServicioProvider
    {
        bool AddReferUser2Service(UsuarioServicioEntity uSE);
        bool UpdateReferUser2Service(int id, UsuarioServicioEntity uSE);
        bool DeleteReferUser2Service(int id);
    }
}
