using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProfileController : Controller {
    public IActionResult Index() {
        if(HttpContext.Session.GetString("IsAuthenticated") != "true") {
            Console.WriteLine("User not loggin in");
            return View("Failed");
        }

        UserDAO userDAO = new UserDAO();
        int ID = (int)HttpContext.Session.GetInt32("ID");

        UserModel user = userDAO.GetUserByID(ID);

        StoreUserTempData(user);

        return View("Index", user);
    }

    public IActionResult Edit() {
        UserModel user = GetUserTempData();
        StoreUserTempData(user);

        return View("Edit", user);
    }

    public IActionResult UpdateUserInfo(UserModel newData) {
        UserDAO userDAO = new UserDAO();

        UserModel oldData = GetUserTempData();

        if(newData.Username != oldData.Username) {
            userDAO.EditUserUsername(oldData, newData.Username);
        }
        if(newData.FirstName != oldData.FirstName) {
            userDAO.EditUserFirstName(oldData, newData.FirstName);
        }
        if(newData.Surname != oldData.Surname) {
            userDAO.EditUserSurname(oldData, newData.Surname);
        }

        return RedirectToAction("Index");
    }

    public void StoreUserTempData(UserModel user) {
        TempData["ID"] = user.ID;
        TempData["Username"] = user.Username;
        TempData["FirstName"] = user.FirstName;
        TempData["Surname"] = user.Surname;
    }

    public UserModel GetUserTempData() {
        return new UserModel { ID = (int)TempData["ID"], 
                            Username = (string)TempData["Username"], 
                            FirstName = (string)TempData["FirstName"], 
                            Surname = (string)TempData["Surname"] };
    }
}