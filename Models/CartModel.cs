using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;
using WebApp.Models;

public class CartModel {
    public List<ProductModel> items { get; set; }
    public int userID { get; set; }

    public CartModel() {
        items = new List<ProductModel>();
    }

    public void Add(ProductModel item) {
        items.Add(item);
    }

    public void Remove(ProductModel item) {
        items.Remove(item);
    }

    public decimal GetFullPrice() {
        decimal result = 0;
        foreach(ProductModel item in items) {
            result += item.Price;
        }
        return result;
    }

    public void PrintItems() {
        Console.WriteLine("Items in Cart:");
        foreach(ProductModel item in items) {
            Console.WriteLine(item.Name);
        }
    }

}