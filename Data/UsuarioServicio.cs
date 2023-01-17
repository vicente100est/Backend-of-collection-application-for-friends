using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class UsuarioServicio
{
    public int IdUs { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdServicio { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
