using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public string? EstadoPedido1 { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
