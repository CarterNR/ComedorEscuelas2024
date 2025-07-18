using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{

    public interface IRolHelper
    {
        List<RolViewModel> GetRoles();

        RolViewModel GetRol(int? id);
        RolViewModel Add(RolViewModel rol);
        RolViewModel Update(RolViewModel rol);
        void Delete(int id);
    }
}