using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Bitacora
{
    public int IdBitacora { get; set; }

    public string? Accion { get; set; }

    public DateTime? FechaHora { get; set; }

    public bool? Estado { get; set; }

    public int? IdEscuela { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
