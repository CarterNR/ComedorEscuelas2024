using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string? NombreEstado { get; set; }

    public virtual ICollection<ProductosDium> ProductosDia { get; set; } = new List<ProductosDium>();
}
