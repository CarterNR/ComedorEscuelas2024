namespace FrontEnd.ApiModels
{
    public class Estudiante
    {
            public int IdEstudiante { get; set; }
            public string Nombre { get; set; }
            public string Cedula { get; set; }
            public int IdEscuela { get; set; } // FK correcta
            public int TiquetesRestantes { get; set; }
            public int IdUsuario { get; set; }
            public byte[] RutaQR { get; set; }
            public string NombreUsuario { get; set; }
            public string Clave { get; set; }

            public DateTime? FechaUltimoRebajo { get; set; }

            // Propiedades de navegación
            public Escuela Escuela { get; set; } // ✔️ relacionada con IdEscuela
            public Usuario Usuario { get; set; } // si aplica
      


    }
}
