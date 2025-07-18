using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class TipoCedulaViewModel
    {

        public int IdTipoCedula { get; set; }

        public string? NombreTipoCedula { get; set; }
      

    }
}