using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class Pago
{
    public string IdPago { get; set; } = null!;

    public decimal PrecioPago { get; set; }

    public int? IdStatus { get; set; }

    public int? IdMensualidad { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Mensualidad? IdMensualidadNavigation { get; set; }

    public virtual StatusP? IdStatusNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
