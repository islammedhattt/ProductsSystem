using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common;
using Persistence.Context;
using Persistance.Common;
using Application.Repositories;

namespace Persistance.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DBContext context) : base(context)
        {

        }

     
    }
}