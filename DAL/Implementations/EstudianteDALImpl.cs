using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EstudianteDALImpl : DALGenericoImpl<Estudiante>, IEstudianteDAL
    {
        SisComedorContext context;

        public EstudianteDALImpl(SisComedorContext context) : base(context)
        {
            this.context = context;
        }

        public Estudiante GetByUsuario(int idUsuario)
        {
            return context.Estudiantes.FirstOrDefault(e => e.IdUsuario == idUsuario);
        }

    }
}
