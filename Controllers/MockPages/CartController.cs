
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

// Controller for mock shop site, deals with cart based aspects
public class CartController : BaseShopController {
    
    public IActionResult AddToCart(ProductModel product, int quantity) {
        CartModel? cart = GetCartFromSession();

        CartItem cartItem = new CartItem(product, quantity);
        cart.Add(cartItem);

        AddCartToSession(cart);

        return RedirectToAction("Index", "Product");
    }

    public IActionResult RemoveFromCart(int productID) {
        CartModel? cart = GetCartFromSession();
        cart.Remove(productID);

        AddCartToSession(cart);

        return RedirectToAction("UserCart");
    }

    // Display user cart
    public IActionResult UserCart() {
        CartModel cart = GetCartFromSession();
        if (cart != null)
            return View("~/Views/MockPages/Products/UserCart.cshtml", cart);
        return View("~/Views/MockPages/Products/UserCart.cshtml", new CartModel());
    }

    // Decreases stock according to items in cart. Clears cart. Shows checkout view
    public IActionResult Checkout() {
        ProductsDAO productsDAO = new ProductsDAO();
        CartModel cart = GetCartFromSession();

        foreach(CartItem item in cart.items) {
            productsDAO.DecreaseStock(item.product);
        }

        ClearCartSession();
        
        return View("~/Views/MockPages/Products/Checkout.cshtml");
    }
}