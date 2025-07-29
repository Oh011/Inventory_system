namespace Infrastructure.Persistence.DataSeeding
{
    public class SupplierSeedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }

}
