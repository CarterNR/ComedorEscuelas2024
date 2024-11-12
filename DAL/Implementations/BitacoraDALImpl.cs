using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class BitacoraDALImpl : DALGenericoImpl<Bitacora>, IBitacoraDAL
    {
        SisComedorContext context;

        public BitacoraDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;

        }
    }
}
