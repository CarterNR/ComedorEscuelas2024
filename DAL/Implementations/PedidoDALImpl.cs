using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class PedidoDALImpl : DALGenericoImpl<Pedido>, IPedidoDAL
    {
        SisComedorContext context;

        public PedidoDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;

        }
    }
}
