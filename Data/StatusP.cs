using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class StatusP
{
    public int IdStatus { get; set; }

    public string? NombreStatus { get; set; }

    public virtual ICollection<Pago> Pagos { get; } = new List<Pago>();
}
