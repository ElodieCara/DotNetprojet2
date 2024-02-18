namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        Product[] GetAllProducts();

        Product GetProductById(int id); // Ajoutez cette méthode 

        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
