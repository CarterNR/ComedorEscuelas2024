using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class TipoCedula
{
    public int IdTipoCedula { get; set; }

    public string? NombreTipoCedula { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
