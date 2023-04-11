namespace Jwt.Models.Response
{
    public class DetailResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public List<Product> Products { get; set; }
    }
}
