﻿using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UsuarioDALImpl : DALGenericoImpl<Usuario>, IUsuarioDAL
    {
        SisComedorContext context;

        public UsuarioDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;

        }
    }
}
