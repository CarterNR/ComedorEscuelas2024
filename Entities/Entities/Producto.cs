using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public int? Cantidad { get; set; }

    public byte[]? Imagen { get; set; }

    public bool? Estado { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdEscuela { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<ProductosDium> ProductosDia { get; set; } = new List<ProductosDium>();
}
