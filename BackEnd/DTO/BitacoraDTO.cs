namespace BackEnd.DTO
{
    public class BitacoraDTO
    {
        public int IdBitacora { get; set; }

        public string? Accion { get; set; }

        public DateTime? FechaHora { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }

        public int? IdUsuario { get; set; }



        // Campo adicional para el nombre de la escuela y proveedor
        public string? NombreCompleto { get; set; }

        public string? NombreEscuela { get; set; }
    }
}
