using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Escuela
{
    public int IdEscuela { get; set; }

    public string? NombreEscuela { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<ProductosDium> ProductosDia { get; set; } = new List<ProductosDium>();

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
