using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioViewModel
    {
        [Display(Name = "ID Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Tipo Cédula")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de cédula")]
        public int IdTipoCedula { get; set; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria")]
        public string Clave { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; }


        [Display(Name = "Correo Eléctronico")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string CorreoElectronico { get; set; }

        public bool? Estado { get; set; }

        [Display(Name = "Escuela")]
        [Required(ErrorMessage = "Debe seleccionar una escuela")]
        public int IdEscuela { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public int IdRol { get; set; }

        // Para mostrar datos asociados
        public string NombreEscuela { get; set; }
        public string NombreRol { get; set; }
        public string NombreTipoCedula { get; set; }

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ListaTiposCedulas { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ListaRoles { get; set; } = new List<SelectListItem>();
    }

}
