using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProductController : BaseShopController {
    public IActionResult Index() {
        ProductsDAO productsDAO = new ProductsDAO();
        List<ProductModel> productsList = productsDAO.GetAllProducts();

        return View("~/Views/MockPages/Products/Index.cshtml", productsList);
    }

    public IActionResult SearchResults(string searchTerm) {
        ProductsDAO productsDAO = new ProductsDAO();
        Console.WriteLine(searchTerm);

        List<ProductModel> productsList = productsDAO.SearchProducts(searchTerm);
        return View("~/Views/MockPages/Products/Index.cshtml", productsList);
    }

    public IActionResult ViewProduct(int productID) {
        ProductsDAO productsDAO = new ProductsDAO();
        ProductModel product = productsDAO.GetProductByID(productID);

        return View("~/Views/MockPages/Products/product.cshtml", product);
    }

    // public IActionResult AddToCart(ProductModel product) {
    //     Console.WriteLine("Buying Item: " + product.Name);

    //     CartModel? cart = GetCartFromSession();
    //     if(cart == null)
    //         cart = new CartModel();

    //     cart.Add(product);

    //     AddCartToSession(cart);

    //     return RedirectToAction("Index");
    // }

    // public IActionResult UserCart() {
    //     CartModel cart = GetCartFromSession();
    //     return View("~/Views/MockPages/Products/UserCart.cshtml", cart.items);
    // }

    // public IActionResult Checkout() {
    //     ProductsDAO productsDAO = new ProductsDAO();
    //     CartModel cart = GetCartFromSession();
    //     foreach(CartItem item in cart.items) {
    //         productsDAO.DecreaseStock(item.product);
    //     }

    //     return RedirectToAction("Index");
    // }

    public IActionResult Update(int ID) {
        ProductsDAO productsDAO = new ProductsDAO();
        ProductModel product = productsDAO.GetProductByID(ID);

        return View("~/Views/MockPages/Products/Update.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ProductModel product)
    {
        ProductsDAO productsDAO = new ProductsDAO();

        if (ModelState.IsValid)
        {
            product.ImageToBinary();

            productsDAO.Update(product);
            return RedirectToAction("Index");    // Redirect to the product list or any other view
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("~/Views/MockPages/Products/Create.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductModel product, IFormFile imageFile)
    {
        ProductsDAO productsDAO = new ProductsDAO();

        if (ModelState.IsValid)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray(); // Convert image to byte array
                }
            }

            productsDAO.Insert(product);  // Call Insert method to save product in database
            return RedirectToAction("Index");    // Redirect to the product list or any other view
        }
        return View(product);
    }
}