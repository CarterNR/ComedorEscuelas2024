using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string? NombreProveedor { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Direccion { get; set; }

    public bool? Estado { get; set; }

    public int? IdEscuela { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
