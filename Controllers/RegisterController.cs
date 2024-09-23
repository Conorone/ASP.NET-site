using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class RegisterController : Controller {
    public IActionResult Index() {
        return View();
    }

    public IActionResult Create() {
        return View();
    }

    public IActionResult ProcessRegistration(UserModel user) {
        UserDAO userDAO = new UserDAO();

        if (!ModelState.IsValid) {
            return View("Create");
        }

        userDAO.CreateUser(user);
            
        return View("Index");

        // foreach (var key in ModelState.Keys)
        // {
        //     var value = ModelState[key];
        //     if (value.Errors.Count > 0)
        //     {
        //         foreach (var error in value.Errors)
        //         {
        //             Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
        //         }
        //     }
        // }
    }

    [HttpPost]
    public IActionResult Create(UserModel registeredUser) {
        return View();
    }
}