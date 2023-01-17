namespace Pagos.Backend.Models.Entity
{
    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }

        public string NombresUsuario { get; set; }

        public string ApellidoUsuario { get; set; }

        public DateOnly FechaNacimientoUsuario { get; set; }

        public string TelefonoUsuario { get; set; }
    }
}
