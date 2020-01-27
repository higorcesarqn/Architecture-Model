namespace Infra.CrossCutting.Jwt.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public int Expires { get; set; }
        public string Type { get; } = "bearer";
        public UserModel User { get; set; }
    }
}