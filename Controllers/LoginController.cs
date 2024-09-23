using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class LoginController : Controller {
    public IActionResult Index() {
        return View();
    }

    public IActionResult ProcessLogin(UserModel user) {
        UserDAO userDAO = new UserDAO();
        UserModel foundUser = userDAO.GetUser(user);

        if (foundUser.ID > 0) {
            setUserSessionData(foundUser);

            Console.WriteLine("UserData:");
            Console.WriteLine("ID: " + HttpContext.Session.GetInt32("ID"));
            Console.WriteLine("username: " + HttpContext.Session.GetString("Username"));
            Console.WriteLine("IsAuthenticated: " + HttpContext.Session.GetString("IsAuthenticated"));

            return View("LoginSuccess", user);
        }
        else {
            return View("LoginFailure", user);
        }
    }

    public void setUserSessionData(UserModel user) {
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("ID", user.ID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("IsAuthenticated", "true");
    }
}