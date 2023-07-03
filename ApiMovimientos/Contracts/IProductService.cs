using ApiMovimientos.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Contracts
{
    public interface IProductService
    {
        public Task<IList<ProductDetails>> ProductListAsync();
        public Task<ProductDetails> GetProductDetailByIdAsync(string productId);
        public Task AddProductAsync(ProductDetails productDetails);
        public Task UpdateProductAsync(string productId, ProductDetails productDetails);
        public Task DeleteProductAsync(string productId);
    }
}
