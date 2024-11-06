using Entities;

namespace ServiceContracts
{
    public interface IProductsService
    {
        public Task<List<Product>> GetAllProducts();

        public Task<Product> GetProductDetails(int? productID);

        public Task AddProduct(Product product);

        public Task EditProduct(Product product);

        public Task<bool> DeleteProduct(int productID);
    }
}
