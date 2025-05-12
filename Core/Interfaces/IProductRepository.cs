using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        // Graphql
        IEnumerable<Product> GetAll();
        Product ProductById(int id);
        void Add(Product product);
        void Save();

    }
}