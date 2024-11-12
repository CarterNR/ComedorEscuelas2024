using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Empleado
{
    public int IdEmpleados { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdTipoCedula { get; set; }

    public string? Cedula { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public bool? Estado { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEscuela { get; set; }

    public virtual Escuela? IdEscuelaNavigation { get; set; }

    public virtual TipoCedula? IdTipoCedulaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
