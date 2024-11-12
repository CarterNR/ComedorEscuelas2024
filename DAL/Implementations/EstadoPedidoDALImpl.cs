using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EstadoPedidoDALImpl : DALGenericoImpl<EstadoPedido>, IEstadoPedidoDAL
    {
        SisComedorContext context;

        public EstadoPedidoDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }
    }
}
