
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

public class CartController : BaseShopController {
    
    public IActionResult AddToCart(ProductModel product) {
        Console.WriteLine("Buying Item: " + product.Name);

        CartModel? cart = GetCartFromSession();
        if(cart == null)
            cart = new CartModel();

        CartItem cartItem = new CartItem();
        cart.Add(cartItem);

        AddCartToSession(cart);

        return RedirectToAction("Index", "Product");
    }

    public IActionResult UserCart() {
        CartModel cart = GetCartFromSession();
        if (cart != null)
            return View("~/Views/MockPages/Products/UserCart.cshtml", cart.items);
        return View("~/Views/MockPages/Products/UserCart.cshtml", new List<CartItem>());
    }

    public IActionResult Checkout() {
        ProductsDAO productsDAO = new ProductsDAO();
        CartModel cart = GetCartFromSession();
        foreach(CartItem item in cart.items) {
            productsDAO.DecreaseStock(item.product);
        }

        return RedirectToAction("Index", "Product");
    }
}