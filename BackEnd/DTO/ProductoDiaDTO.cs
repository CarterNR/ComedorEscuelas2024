namespace BackEnd.DTO
{
    public class ProductoDiaDTO
    {
        public int IdProductoDia { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? Fecha { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }



        // Campo adicional para el nombre de la escuela
        public string? NombreEscuela { get; set; }
        public string? NombreProducto { get; set; }
        
    }
}
