
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

public class CartController : BaseShopController {
    
    public IActionResult AddToCart(ProductModel product) {
        Console.WriteLine("Buying Item: " + product.Name);

        CartModel? cart = GetCartFromSession();
        if(cart == null)
            cart = new CartModel();

        cart.Add(product);

        AddCartToSession(cart);

        return RedirectToAction("Index");
    }

    public IActionResult UserCart() {
        CartModel cart = GetCartFromSession();
        return View("~/Views/MockPages/Products/UserCart.cshtml", cart.items);
    }

    public IActionResult Checkout() {
        ProductsDAO productsDAO = new ProductsDAO();
        CartModel cart = GetCartFromSession();
        foreach(CartItem item in cart.items) {
            productsDAO.DecreaseStock(item.product);
        }

        return RedirectToAction("Index");
    }
}