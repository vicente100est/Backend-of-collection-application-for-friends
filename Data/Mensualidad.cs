using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class Mensualidad
{
    public int IdMensualidad { get; set; }

    public string NombreMensualidad { get; set; } = null!;

    public decimal PrecioMensualidad { get; set; }

    public int? IdServicio { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; } = new List<Pago>();
}
