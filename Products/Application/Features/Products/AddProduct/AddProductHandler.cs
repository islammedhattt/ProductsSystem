using Application.Common.Repositories;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public AddProductHandler(
            IUnitOfWork unitOfWork,
            IProductRepository ProductRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            var Product = _mapper.Map<Product>(request);
            _ProductRepository.Create(Product);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<AddProductResponse>(Product);
        }
    }
}
