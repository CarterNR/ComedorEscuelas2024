namespace BackEnd.DTO
{
    public class EstudianteDTO
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

    }
}
