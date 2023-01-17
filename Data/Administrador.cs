using System;
using System.Collections.Generic;

namespace Pagos.Backend.Data;

public partial class Administrador
{
    public int IdAdministrador { get; set; }

    public string NombreAdministrador { get; set; } = null!;

    public string ContrasenaAdministrador { get; set; } = null!;
}
