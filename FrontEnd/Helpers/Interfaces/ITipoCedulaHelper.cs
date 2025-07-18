using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ITipoCedulaHelper
    {
        List<TipoCedulaViewModel> GetTiposCedulas();

        TipoCedulaViewModel GetTipoCedula(int? id);
        TipoCedulaViewModel Add(TipoCedulaViewModel tipocedula);
        TipoCedulaViewModel Update(TipoCedulaViewModel tipocedula);
        void Delete(int id);
    }
}