using System.Threading.Tasks;
using Application.Features.Products.AddProduct;
using Application.Features.Products.DeleteProduct;
using Application.Features.Products.GetAllProducts;
using Application.Features.Products.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth;

namespace PresentationApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetProduct")]
        public async Task<GetProductByIdResponse> GetProduct(GetProductByIdRequest request)
        {
            return await _mediator.Send(request);
        }
      

        [HttpPost("AddProduct")]
        //[Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("GetAll")]
        public async Task<List<GetAllProductsResponse>> GetAll(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("GetProductById")]
        public async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("UpdateProduct")]
        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("DeleteProduct")]
        public async Task DeleteProduct(DeleteProductRequest request)
        {
             await _mediator.Send(request);
        }
    }
}
