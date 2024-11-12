using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IEstadoPedidoService
    {
        bool Agregar(EstadoPedidoDTO estadoPedido);
        bool Editar(EstadoPedidoDTO estadoPedido);
        bool Eliminar(EstadoPedidoDTO estadoPedido);
        EstadoPedidoDTO Obtener(int id);
        List<EstadoPedidoDTO> Obtener();
    }
}
