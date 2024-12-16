using System.Data;

namespace BackEnd.DTO
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
