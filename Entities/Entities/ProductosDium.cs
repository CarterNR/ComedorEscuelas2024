using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class ProductosDium
{
    public int IdProductoDia { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Estado { get; set; }

    public int? IdEscuela { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
