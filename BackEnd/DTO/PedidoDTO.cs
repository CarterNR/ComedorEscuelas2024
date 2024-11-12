namespace BackEnd.DTO
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        public int? IdProducto { get; set; }

        public DateTime? FechaHoraIngreso { get; set; }

        public int? Cantidad { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdEscuela { get; set; }

        public int? IdEstadoPedido { get; set; }

        public bool? Estado { get; set; }



        // Campo adicional para el nombre de la escuela y proveedor
        public string? NombreProducto { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NombreEscuela { get; set; }
        public string? EstadoPedido1 { get; set; }
        public string? NombreProveedor { get; set; }

    }
}
