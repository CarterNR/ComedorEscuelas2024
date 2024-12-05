using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IEstadoPedidoHelper
    {
        List<EstadoPedidoViewModel> GetEstadoPedidos();

        EstadoPedidoViewModel GetEstadoPedido(int? id);
        EstadoPedidoViewModel Add(EstadoPedidoViewModel estadopedido);
        EstadoPedidoViewModel Update(EstadoPedidoViewModel estadopedido);
        void Delete(int id);
    }
}