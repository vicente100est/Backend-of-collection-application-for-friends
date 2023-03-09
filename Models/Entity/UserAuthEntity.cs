namespace Pagos.Backend.Models.Entity
{
    public class UserAuthEntity
    {
        public DateOnly FechaNacimientoUsuario { get; set; }

        public string TelefonoUsuario { get; set; } = null!;
    }
}
