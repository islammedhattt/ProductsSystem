using Application.Common.Exceptions;
using Application.Common.Repositories;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.AddProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(
            IUnitOfWork unitOfWork,
            IProductRepository ProductRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var Product =  await _ProductRepository.GetById(request.Id, cancellationToken);
            _mapper.Map(request, Product);
            _ProductRepository.Update(Product);
            await _unitOfWork.Save(cancellationToken);
            return new UpdateProductResponse();
        }
    }
}
