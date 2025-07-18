using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class RolDALImpl : DALGenericoImpl<Rol>, IRolDAL
    {
        SisComedorContext context;

        public RolDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }
    }
}
