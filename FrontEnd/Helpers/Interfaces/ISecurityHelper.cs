using FrontEnd.ApiModels;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ISecurityHelper
    {
        LoginAPI Login(UserViewModel user);

        bool Register(RegisterAPI registerModel);

        void Logout();

        List<UserAPI> GetAllUsers(); 

        UserAPI GetUserDetails(string id); 
        bool UpdateUser(UsersViewModel user); 
        bool DeleteUser(string id);
    }
}
