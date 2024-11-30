namespace Application.Features.Products.GetAllProducts
{
    public sealed record GetAllProductsResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; } = 0;
        public string? Description { get; set; }
    }
}