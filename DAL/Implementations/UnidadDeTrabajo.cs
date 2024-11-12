using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public ITipoCedulaDAL TipoCedulaDAL { get; set; }
        public IRolDAL RolDAL { get; set; }
        public IEstadoPedidoDAL EstadoPedidoDAL { get; set; }
        public IEscuelaDAL EscuelaDAL {  get; set; }
        public IProveedorDAL ProveedorDAL { get; set; }
        public IProductoDAL ProductoDAL { get; set; }
        public IProductoDiaDAL ProductoDiaDAL { get; set; }
        public IUsuarioDAL UsuarioDAL { get; set; }
        public IBitacoraDAL BitacoraDAL { get; set; }
        public IPedidoDAL PedidoDAL { get; set; }




       
        private SisComedorContext _sisComedorContext;

        public UnidadDeTrabajo(SisComedorContext sisComedorContext,
                         ITipoCedulaDAL tipoCedulaDAL, 
                         IRolDAL rolDAL, IEstadoPedidoDAL estadoPedidosDAL, 
                         IEscuelaDAL escuelaDAL, IProveedorDAL proveedorDAL,
                         IProductoDAL productoDAL, IProductoDiaDAL productoDiaDAL,
                         IUsuarioDAL usuarioDAL, IBitacoraDAL bitacoraDAL,
                         IPedidoDAL pedidoDAL

             )
        {
            this._sisComedorContext = sisComedorContext;
            this.TipoCedulaDAL = tipoCedulaDAL;
            this.RolDAL = rolDAL;
            this.EscuelaDAL = escuelaDAL;
            this.EstadoPedidoDAL = estadoPedidosDAL;
            this.ProveedorDAL = proveedorDAL;
            this.ProductoDAL = productoDAL;
            this.ProductoDiaDAL = productoDiaDAL;
            this.UsuarioDAL = usuarioDAL;
            this.BitacoraDAL = bitacoraDAL;
            this.PedidoDAL = pedidoDAL;
        }


        public bool Complete()
        {
            try
            {
                _sisComedorContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
        {
            this._sisComedorContext.Dispose();
        }
    }
}
