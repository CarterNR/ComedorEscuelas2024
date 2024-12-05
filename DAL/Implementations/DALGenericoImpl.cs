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
    public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
    {
        

        private SisComedorContext _sisComedorContext;

        public DALGenericoImpl(SisComedorContext sisComedorContext)
        {

            _sisComedorContext = sisComedorContext;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _sisComedorContext.Add(entity);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public TEntity Get(int id)
        {

            return _sisComedorContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _sisComedorContext.Set<TEntity>().ToList();
        }


        public bool Remove(TEntity entity)
        {
            try
            {
                _sisComedorContext.Set<TEntity>().Attach(entity);
                _sisComedorContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando entidad: {ex.Message}");
                return false;
            }
        }



        public bool Update(TEntity entity)
        {
            try
            {
                _sisComedorContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
