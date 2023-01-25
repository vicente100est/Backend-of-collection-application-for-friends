using Pagos.Backend.Data;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.DAL.IServices
{
    public interface IUsuarioProvider
    {
        Task<ICollection<Usuario>> GetUserAsync();
        Task<Usuario> GetUserByIdAsync(int id);
        object GetUsersService(int id);
        bool CreateUser(UsuarioEntity usuario);
        bool UpdateUser(int id, UsuarioEntity usuario);
        bool DeleteUser(int id);
    }
}
