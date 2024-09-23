using WebApp.Models;

public class SecurityService {
    UserDAO userDAO = new UserDAO();

    public SecurityService() {
        
    }

    public bool isValid(UserModel user) {
        return userDAO.FindUser(user);
    }
}