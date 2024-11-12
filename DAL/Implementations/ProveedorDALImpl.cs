using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ProveedorDALImpl : DALGenericoImpl<Proveedore>, IProveedorDAL
    {
        SisComedorContext context;

        public ProveedorDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }
    }
}
