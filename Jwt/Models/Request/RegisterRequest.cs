namespace Jwt.Models.Request
{
    public class RegisterRequest
    {

        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
