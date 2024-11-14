using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IEscuelaHelper
    {
        List<EscuelaViewModel> GetEscuelas();

        EscuelaViewModel GetEscuela(int? id);
        EscuelaViewModel Add(EscuelaViewModel escuela);
        EscuelaViewModel Update(EscuelaViewModel escuela);
        void Delete(int id);
    }
}