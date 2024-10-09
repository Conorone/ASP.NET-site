using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;
using WebApp.Models;

namespace WebApp.Models;

public class CartModel {
    public List<CartItem> items { get; set; }
    public int userID { get; set; }

    public CartModel() {
        items = new List<CartItem>();
    }

    public int GetItemCount() {
        int count = 0;
        foreach(CartItem cartItem in items) {
            count += cartItem.quantity;
        }
        return count;
    }

    public void Add(CartItem item) {
        foreach(CartItem cartItem in items) {
            if (item.product.ID == cartItem.product.ID) {
                cartItem.Increment();
                return;
            }
        }
        items.Add(item);
    }

        public void Add(ProductModel item) {
        Add(new CartItem() {product = item});
    }

    public void Remove(ProductModel item) {
        items.Remove(new CartItem() {product = item});
    }

    public decimal GetFullPrice() {
        decimal result = 0;
        foreach(CartItem item in items) {
            result += item.GetPrice;
        }
        return result;
    }

    public void PrintItems() {
        Console.WriteLine("Items in Cart:");
        foreach(CartItem item in items) {
            Console.WriteLine(item.product.Name);
        }
    }

}