using Application.Common.Exceptions;
using Application.Common.Repositories;
using Application.Features.Products.AddProduct;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest , DeleteProductResponse>
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private IConfiguration _configuration;
        private readonly IMapper _mapper;


        public DeleteProductHandler(
            IUnitOfWork unitOfWork,
            IProductRepository ProductRepository,
            IMapper mapper)        {
            _unitOfWork = unitOfWork;
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetById(request.Id, cancellationToken);
            if (product == null)
                throw new NotFoundException("product id is not exist!");

            _ProductRepository.Delete(product);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<DeleteProductResponse>(product);
        }

    }
}
