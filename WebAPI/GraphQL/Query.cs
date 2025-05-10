using Core.Entities;
using Core.Interfaces;

namespace GraphQLApi.GraphQL
{
    public class Query
    {
        private readonly IProductRepository _productRepo;

        public Query(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetProducts() => _productRepo.GetAll();
    }
}
