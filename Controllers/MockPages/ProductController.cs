using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ProductController : Controller {
    public IActionResult Index() {
        ProductsDAO productsDAO = new ProductsDAO();
        List<ProductModel> productsList = productsDAO.GetAllProducts();

        return View("~/Views/MockPages/Products/Index.cshtml", productsList);
    }

    public IActionResult SearchResults(string searchTerm) {
        ProductsDAO productsDAO = new ProductsDAO();

        List<ProductModel> productsList = productsDAO.SearchProducts(searchTerm);
        return View("~/Views/MockPages/Products/Index.cshtml", productsList);
    }

    public IActionResult SearchForm() {
        return View("~/Views/MockPages/Products/_SearchBar.cshtml");
    }

    public IActionResult ViewProduct(int productID) {
        ProductsDAO productsDAO = new ProductsDAO();

        ProductModel product = productsDAO.GetProductByID(productID);
        return View("~/Views/MockPages/Products/product.cshtml", product);
    }

    public IActionResult AddToCart(ProductModel product) {
        Console.WriteLine("Buying Item: " + product.Name);

        string? productsJson = HttpContext.Session.GetString("CartProducts");
        CartModel cart = new CartModel();
        
        if (!string.IsNullOrEmpty(productsJson)) {
            Console.WriteLine("Items found in cart!");
            cart = JsonConvert.DeserializeObject<CartModel>(productsJson);
        }

        cart.Add(product);

        Console.WriteLine("Items in Cart:");
        foreach(ProductModel item in cart.items) {
            Console.WriteLine(item.Name);
        }

        productsJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("CartProducts", productsJson);

        return RedirectToAction("Index");
    }

    public IActionResult UserCart() {
        CartModel cart = GetCartFromSession();
        return View("~/Views/MockPages/Products/UserCart.cshtml", cart.items);
    }

    public CartModel? GetCartFromSession() {
        string? productsJson = HttpContext.Session.GetString("CartProducts"); 
        return JsonConvert.DeserializeObject<CartModel>(productsJson);
    }

    public void AddCartToSession(CartModel cart) {
        string productsJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("CartProducts", productsJson);
    }

    public IActionResult Checkout() {
        ProductsDAO productsDAO = new ProductsDAO();
        CartModel cart = GetCartFromSession();
        foreach(ProductModel product in cart.items) {
            productsDAO.DecreaseStock(product);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Stock() {
        return View("~/Views/MockPages/Products/SetStock.cshtml");
    }

    public IActionResult SetStock(int productID, int stock) {
        Console.WriteLine("ProductID: " + productID + " Stock: " + stock);
        ProductsDAO productsDAO = new ProductsDAO();

        ProductModel product = productsDAO.GetProductByID(productID);
        productsDAO.SetStock(product, stock);

        return View("~/Views/MockPages/Products/SetStock.cshtml");
    }

    public IActionResult Update(int ID) {
        ProductsDAO productsDAO = new ProductsDAO();
        ProductModel product = productsDAO.GetProductByID(ID);

        return View("~/Views/MockPages/Products/Update.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProduct(ProductModel product, IFormFile? imageFile)
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