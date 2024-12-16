using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")] 
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")] 
        public string Password { get; set; }
        
        public TokenModel? Token { get; set; }

        public List<string>? Roles { get; set; }
    }
}
