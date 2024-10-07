using WebApp.Models;

namespace WebApp.Models;

public class CartItem {
    public ProductModel product { get; set; }
    public int quantity { get; set; }

    public CartItem() {
        product = new ProductModel();
        quantity = 1;
    }

    public void Increment(int quantity) {
        this.quantity += quantity;
    }

    public void Increment() {
        Increment(1);
    }

    public decimal GetPrice => product.Price * quantity;
}