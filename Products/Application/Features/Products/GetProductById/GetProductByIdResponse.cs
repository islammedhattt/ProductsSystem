namespace Application.Features.Products.GetProductById
{
    public sealed record GetProductByIdResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }

    }
}