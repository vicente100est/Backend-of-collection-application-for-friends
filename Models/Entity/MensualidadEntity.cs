namespace Pagos.Backend.Models.Entity
{
    public class MensualidadEntity
    {
        public int IdMensualidad { get; set; }

        public string NombreMensualidad { get; set; }

        public decimal PrecioMensualidad { get; set; }

        public int IdServicio { get; set; }
    }
}
