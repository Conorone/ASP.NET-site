using System.ComponentModel.DataAnnotations;

public class UniqueAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        UserDAO userDAO = new UserDAO();
        var stringValue = value as string;

        if (stringValue != null) {
            if(userDAO.FindUser(stringValue)) {
                return new ValidationResult($"Value already taken.");
            }
        }
        return ValidationResult.Success;
    }
}