using Application.Services;
using Core.Entities;

namespace GraphQLApi.GraphQL
{
    public class Mutation
    {
        private readonly IProductService _productService;

        public Mutation(IProductService productService)
        {
            _productService = productService;
        }

        public Product AddProduct(string name, decimal price)
        {
            var product = new Product { Name = name, Price = price };
            _productService.Add(product);
            return product;
        }
    }
}
