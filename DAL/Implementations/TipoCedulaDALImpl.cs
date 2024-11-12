using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class TipoCedulaDALImpl : DALGenericoImpl<TipoCedula>, ITipoCedulaDAL
    {
        SisComedorContext context;

        public TipoCedulaDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }
    }
}
