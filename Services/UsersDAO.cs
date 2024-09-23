using WebApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class UserDAO {
    private bool success = false;
    private string table = "dbo.Users";

    string connectionString = @"Data Source=localhost;Initial Catalog = Test; Integrated Security = True; 
    Encrypt=True;TrustServerCertificate=True;";

    public bool FindUser(UserModel user) {
        string sqlStatement = $"SELECT * FROM {table} WHERE username = @username AND password = @password";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
            command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Searching for user...");

                if (reader.HasRows) {
                    success = true;
                    Console.WriteLine("User found");
                }

            } catch(Exception e) {
                Console.WriteLine("Error in FindUser database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }

    public bool FindUser(string username) {
        string sqlStatement = $"SELECT * FROM {table} WHERE username = @username";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = username;

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Searching for user...");

                if (reader.HasRows) {
                    success = true;
                    Console.WriteLine("User found");
                }

            } catch(Exception e) {
                Console.WriteLine("Error in FindUser database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }

    public UserModel GetUser(UserModel user) {
        UserModel foundUser = new UserModel();

        string sqlStatement = $"SELECT * FROM {table} WHERE username = @username AND password = @password";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
            command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Searching for user...");

                if (reader.HasRows) {
                    reader.Read();
                    foundUser = new UserModel { ID = (int)reader[0], Username = (string)reader[1], Password = (string)reader[2], FirstName = (string)reader[3], Surname = (string)reader[4] };
                    Console.WriteLine("User found");
                }

            } catch(Exception e) {
                Console.WriteLine("Error in FindUser database connection");
                Console.WriteLine(e.Message);
            }
        }
        return foundUser;
    }

    public UserModel GetUserByID(int ID) {
        UserModel user = new UserModel();

        string sqlStatement = $"SELECT * FROM {table} WHERE Id = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    reader.Read();
                    user = new UserModel { ID = (int)reader[0], Username = (string)reader[1], Password = (string)reader[2], FirstName = (string)reader[3], Surname = (string)reader[4] };
                }

            } catch(Exception e) {
                Console.WriteLine("Error in GetUserByID database connection");
                Console.WriteLine(e.Message);
            }
        }
        return user;
    }

    public bool CreateUser(UserModel user) {
        string sqlStatement = $"INSERT INTO {table} (USERNAME, PASSWORD, FIRSTNAME, SURNAME) VALUES (@username, @password, @firstname, @surname)";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
            command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;
            command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 40).Value = user.FirstName;
            command.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 40).Value = user.Surname;

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Creating user...");

                if (FindUser(user)) {
                    success = true;
                    Console.WriteLine("User creation confirmed");
                }                
            } catch(Exception e) {
                Console.WriteLine("Error in Registration database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }

    public bool EditUserUsername(UserModel user, string newName) {
        string sqlStatement = $"UPDATE {table} SET username = @newName WHERE Id = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@ID", user.ID);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                success = true;
                Console.WriteLine("Username updated");
              
            } catch(Exception e) {
                Console.WriteLine("Error in Registration database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }

    public bool EditUserFirstName(UserModel user, string newName) {
        string sqlStatement = $"UPDATE {table} SET FirstName = @newName WHERE Id = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@ID", user.ID);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                success = true;
                Console.WriteLine("First name updated");
              
            } catch(Exception e) {
                Console.WriteLine("Error in Registration database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }

    public bool EditUserSurname(UserModel user, string newName) {
        string sqlStatement = $"UPDATE {table} SET Surname = @newName WHERE Id = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@ID", user.ID);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                success = true;
                Console.WriteLine("Surname updated");
              
            } catch(Exception e) {
                Console.WriteLine("Error in Registration database connection");
                Console.WriteLine(e.Message);
            }
        }
        return success;
    }
}