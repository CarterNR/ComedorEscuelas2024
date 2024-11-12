using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class TipoCedula
{
    public int IdTipoCedula { get; set; }

    public string? TipoCedula1 { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
