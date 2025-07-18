using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Usuario
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

        public virtual Escuela Escuela { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual TipoCedula TipoCedula { get; set; }

        public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }




