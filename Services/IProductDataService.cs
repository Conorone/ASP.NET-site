using WebApp.Models;

namespace WebApp.Services;

public interface IProductDataService {
    List<ProductModel> GetAllProducts();
    List<ProductModel> SearchProducts(string searchTerm);
    ProductModel GetProductByID(int ID);
    bool Insert(ProductModel product);
    int Delete(ProductModel product);
    bool Update(ProductModel product);
}