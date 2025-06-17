using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }
    public string Nombre { get; set; }
    public string Cedula { get; set; }
    public string Contrasena { get; set; }
    public int TiquetesRestantes { get; set; }

}
