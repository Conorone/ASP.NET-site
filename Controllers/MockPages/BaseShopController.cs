using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApp.Models;

public class BaseShopController : Controller
{
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

    public CartModel? GetCartFromSession() {
        string? productsJson = HttpContext.Session.GetString("CartProducts");
        if (productsJson != null)
            return JsonConvert.DeserializeObject<CartModel>(productsJson);
        else return null;
    }
    public void AddCartToSession(CartModel cart) {
        string productsJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("CartProducts", productsJson);
    }
}