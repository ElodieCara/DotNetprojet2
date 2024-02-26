namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IProductService
    {
        Product[] GetAllProducts();
        Product GetProductById(int id);
        void UpdateProductQuantities(Cart cart);
        void UpdateProduct(Product product); 
    }
}
