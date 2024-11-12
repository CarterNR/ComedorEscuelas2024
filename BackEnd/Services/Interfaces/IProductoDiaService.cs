using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProductoDiaService
    {
        bool Agregar(ProductoDiaDTO productoDia);
        bool Editar(ProductoDiaDTO productoDia);
        bool Eliminar(ProductoDiaDTO productoDia);
        ProductoDiaDTO Obtener(int id);
        List<ProductoDiaDTO> Obtener();
    }
}
