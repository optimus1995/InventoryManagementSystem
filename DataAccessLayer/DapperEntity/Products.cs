namespace ApplicationCore.DapperEntity
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SKU { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public string? UserId { get; set; }
        public Category? Category { get; set; }
        public AspNetUsers? AspNetUsers { get; set; }
        public ProductImages? ProductImages{ get; set; }
    }
}
