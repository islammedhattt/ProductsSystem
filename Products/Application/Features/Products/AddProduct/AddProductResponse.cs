namespace Application.Features.Products.AddProduct
{
    public sealed record AddProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}