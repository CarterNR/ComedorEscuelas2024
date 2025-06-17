using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class EstudianteViewModel
    {

        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Contrasena { get; set; }
        public int TiquetesRestantes { get; set; }




    }
}