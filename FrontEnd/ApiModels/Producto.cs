namespace FrontEnd.ApiModels
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int? Cantidad { get; set; }
        public byte[]? Imagen { get; set; }
        public bool? Estado { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdEscuela { get; set; }
        public string? NombreProveedor { get; set; }
        public string? NombreEscuela { get; set; }

    }
}
