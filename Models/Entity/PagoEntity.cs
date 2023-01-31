namespace Pagos.Backend.Models.Entity
{
    public class PagoEntity
    {
        public string IdPago { get; set; } = Guid.NewGuid().ToString();

        public decimal PrecioPago { get; set; }

        public int IdStatus { get; set; }

        public int IdMensualidad { get; set; }

        public int IdUsuario { get; set; }
    }
}
