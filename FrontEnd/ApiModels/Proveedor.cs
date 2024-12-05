namespace FrontEnd.ApiModels
{
    public class Proveedor
    {

        public int IdProveedor { get; set; }

        public string? NombreProveedor { get; set; }

        public string? Telefono { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Direccion { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }


        // Campo adicional para el nombre de la escuela
        public string? NombreEscuela { get; set; }

    }

}

