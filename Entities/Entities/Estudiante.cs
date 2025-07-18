using System;
using System.Collections.Generic;

namespace Entities.Entities;

public class Estudiante
{

        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int IdEscuela { get; set; }
        public int TiquetesRestantes { get; set; }
        public int IdUsuario { get; set; }
        public byte[] RutaQR { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }

        public DateTime? FechaUltimoRebajo { get; set; }

        public virtual Usuario Usuario { get; set; }  // Relación con Usuario

        // Relación con ESCUELAS
        public Escuela Escuela { get; set; }



}

