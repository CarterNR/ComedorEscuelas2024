using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo: IDisposable
    {
        ITipoCedulaDAL TipoCedulaDAL { get; }
        IRolDAL RolDAL { get; }
        IEstadoPedidoDAL EstadoPedidoDAL { get; }
        IEscuelaDAL EscuelaDAL { get; }
        IProveedorDAL ProveedorDAL { get; }
        IProductoDAL ProductoDAL { get; }
        IProductoDiaDAL ProductoDiaDAL { get; }
        IUsuarioDAL UsuarioDAL { get; }
        IBitacoraDAL BitacoraDAL { get; }
        IPedidoDAL PedidoDAL { get; }





       
        bool Complete();
    }
}
