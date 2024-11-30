using Application.Common.Repositories;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.GetAllProducts
{
    public sealed class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, List<GetAllProductsResponse>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IUnitOfWork unitOfWork, IProductRepository ProductRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var Product = await _ProductRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllProductsResponse>>(Product);
        }
    }
}