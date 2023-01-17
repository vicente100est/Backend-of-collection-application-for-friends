using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; } = null!;

    public decimal PrecioServicio { get; set; }

    public virtual ICollection<Mensualidad> Mensualidads { get; } = new List<Mensualidad>();

    public virtual ICollection<UsuarioServicio> UsuarioServicios { get; } = new List<UsuarioServicio>();
}
