namespace Application.Features.Products.AddProduct
{
    public class UpdateProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}
