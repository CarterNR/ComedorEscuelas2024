using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? FechaHoraIngreso { get; set; }

    public int? Cantidad { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEscuela { get; set; }

    public int? IdEstadoPedido { get; set; }

    public bool? Estado { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual EstadoPedido? IdEstadoPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
