using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApp.Models;

// Base controller class inhereted by controllers in the mock shop site
public class BaseShopController : Controller
{
    // Initialises value for cart number displayed on top bar
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        int count = 0;
        CartModel cart = GetCartFromSession();
        if (cart != null) {
            count = cart.GetItemCount();
        }

        ViewBag.cartCount = count;

        base.OnActionExecuting(filterContext);
    }

    // Methods for dealing with shopping cart session
    public CartModel? GetCartFromSession() {
        string? productsJson = HttpContext.Session.GetString("CartProducts");
        if (productsJson != null)
            return JsonConvert.DeserializeObject<CartModel>(productsJson);
        else return new CartModel();
    }
    public void AddCartToSession(CartModel cart) {
        string productsJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("CartProducts", productsJson);
    }
    public void ClearCartSession() {
        HttpContext.Session.Remove("CartProducts");
    }
}