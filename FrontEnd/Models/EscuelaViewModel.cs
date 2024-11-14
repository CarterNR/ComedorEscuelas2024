using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class EscuelaViewModel
    {

        public int IdEscuela { get; set; }

        public string? NombreEscuela { get; set; }


        public bool? Estado { get; set; }



    }
}