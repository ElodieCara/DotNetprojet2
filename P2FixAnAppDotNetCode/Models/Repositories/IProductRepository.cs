namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        Product[] GetAllProducts();

        Product GetProductById(int id); 

        void UpdateProductStocks(int productId, int quantityToRemove);

        void UpdateProduct(Product product); 
    }
}
