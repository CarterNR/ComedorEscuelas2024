using FrontEnd.ApiModels;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ISecurityHelper
    {
        bool Register(RegisterAPI registerModel);

        void Logout();

        bool DeleteUser(string id);
    }
}
