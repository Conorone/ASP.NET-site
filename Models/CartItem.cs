using WebApp.Models;

namespace WebApp.Models;

// Simple class that pares product and quantity of product
public class CartItem {
    public ProductModel product { get; set; }
    public int quantity { get; set; }

    public CartItem() {
        product = new ProductModel();
        quantity = 1;
    }

    public CartItem(ProductModel product, int quantity) {
        this.product = product;
        this.quantity = quantity;
    }

    public void Increment(int quantity) {
        this.quantity += quantity;
    }

    public void Increment() {
        Increment(1);
    }

    public void Decrement(int quantity) {
        this.quantity -= quantity;
    }

    public void Decrement() {
        Decrement(1);
    }

    public decimal GetPrice => product.Price * quantity;
}