using Entities.Entities;

namespace BackEnd.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public int IdTipoCedula { get; set; }
        public string Cedula { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public bool? Estado { get; set; }
        public int IdEscuela { get; set; }
        public int IdRol { get; set; }

        public string? NombreEscuela { get; set; }
        public string? NombreTipoCedula { get; set; }
        public string? NombreRol { get; set; }
    }
}

