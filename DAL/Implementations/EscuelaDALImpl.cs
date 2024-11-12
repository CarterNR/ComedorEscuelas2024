using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EscuelaDALImpl : DALGenericoImpl<Escuela>, IEscuelaDAL
    {
        SisComedorContext context;

        public EscuelaDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }
    }
}
