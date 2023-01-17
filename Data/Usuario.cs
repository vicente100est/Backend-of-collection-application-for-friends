using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombresUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public DateOnly FechaNacimientoUsuario { get; set; }

    public string TelefonoUsuario { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; } = new List<Pago>();

    public virtual ICollection<UsuarioServicio> UsuarioServicios { get; } = new List<UsuarioServicio>();
}
