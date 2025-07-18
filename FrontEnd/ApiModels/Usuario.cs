using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.ApiModels
{
    public class Usuario
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

    //    public Estudiante Estudiante { get; set; }

        public Escuela Escuela { get; set; }
        public Rol Rol { get; set; }
        
        public TipoCedula TipoCedula { get; set; }


    }

}


