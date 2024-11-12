using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class EstadoPedidoService : IEstadoPedidoService
    {
        IUnidadDeTrabajo Unidad;
        public EstadoPedidoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region
        EstadoPedido Convertir(EstadoPedidoDTO estadoPedido)
        {
            return new EstadoPedido
            {
                IdEstadoPedido = estadoPedido.IdEstadoPedido,
                EstadoPedido1 = estadoPedido.EstadoPedido1
            };
        }

        EstadoPedidoDTO Convertir(EstadoPedido estadoPedido)
        {
            return new EstadoPedidoDTO
            {
                IdEstadoPedido = estadoPedido.IdEstadoPedido,
                EstadoPedido1 = estadoPedido.EstadoPedido1
            };
        }
        #endregion




        public bool Agregar(EstadoPedidoDTO estadoPedido)
        {
            EstadoPedido entity = Convertir(estadoPedido);
            Unidad.EstadoPedidoDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(EstadoPedidoDTO estadoPedido)
        {
            var entity = Convertir(estadoPedido);
            Unidad.EstadoPedidoDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(EstadoPedidoDTO estadoPedido)
        {
            var entity = Convertir(estadoPedido);
            Unidad.EstadoPedidoDAL.Remove(entity);
            return Unidad.Complete();
        }

        public EstadoPedidoDTO Obtener(int id)
        {
            return Convertir(Unidad.EstadoPedidoDAL.Get(id));
        }

        public List<EstadoPedidoDTO> Obtener()
        {
            List<EstadoPedidoDTO> list = new List<EstadoPedidoDTO>();
            var estadoPedidos = Unidad.EstadoPedidoDAL.GetAll().ToList();

            foreach (var item in estadoPedidos)
            {
                list.Add(Convertir(item));
            }
            return list;
        }
    }
}
