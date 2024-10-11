
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

public class CartController : BaseShopController {
    
    public IActionResult AddToCart(ProductModel product, int quantity) {
        Console.WriteLine("Buying Item: " + product.Name);

        CartModel? cart = GetCartFromSession();

        CartItem cartItem = new CartItem(product, quantity);
        cart.Add(cartItem);

        AddCartToSession(cart);

        return RedirectToAction("Index", "Product");
    }

    public IActionResult RemoveFromCart(ProductModel product, int quantity) {
        CartModel? cart = GetCartFromSession();
        cart.Remove(product);

        AddCartToSession(cart);

        return RedirectToAction("UserCart");
    }

    public IActionResult UserCart() {
        CartModel cart = GetCartFromSession();
        if (cart != null)
            return View("~/Views/MockPages/Products/UserCart.cshtml", cart);
        return View("~/Views/MockPages/Products/UserCart.cshtml", new CartModel());
    }

    public IActionResult Checkout() {
        ProductsDAO productsDAO = new ProductsDAO();
        CartModel cart = GetCartFromSession();


        foreach(CartItem item in cart.items) {
            productsDAO.DecreaseStock(item.product);
        }

        return View("~/Views/MockPages/Products/Checkout.cshtml");
    }
}