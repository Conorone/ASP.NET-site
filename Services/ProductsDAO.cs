using System.Runtime.InteropServices.ObjectiveC;
using Microsoft.Data.SqlClient;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Services;

public class ProductsDAO : IProductDataService
{
    readonly string table = "dbo.Products";

    string connectionString = @"Data Source=localhost;Initial Catalog = Test; Integrated Security = True; 
    Encrypt=True;TrustServerCertificate=True;";

    public int Delete(ProductModel product)
    {
        throw new NotImplementedException();
    }

    public List<ProductModel> GetProducts(string column, object value)
    {
        List<ProductModel> foundProducts = new List<ProductModel>();

        string sqlStatement = $"SELECT * FROM {table} WHERE {column} = @value";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);
            command.Parameters.AddWithValue("@value", value);

            Console.WriteLine("Statement: " );

            string finalSql = command.CommandText;
            foreach (SqlParameter param in command.Parameters)
            {
                finalSql = finalSql.Replace(param.ParameterName, param.Value.ToString());
            }

            // Write the completed command statement to the console
            Console.WriteLine("Statement: " + finalSql);

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    ProductModel product = new ProductModel { ID = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3], Stock = (int)reader[4] };
                    product.Image = reader[5] == DBNull.Value ? null : (byte[])reader[5];
                    foundProducts.Add(product);
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        return foundProducts;
    }

    public List<ProductModel> GetAllProducts() {
        // SQL statement 1 = 1 returns all products
        return GetProducts("1", "1");
    }

    public ProductModel GetProductByID(int ID) {
        return GetProducts("ID", ID).First();
    }

    public List<ProductModel> SearchProducts(string searchTerm) {
        List<ProductModel> foundProducts = new List<ProductModel>();

        string sqlStatement = $"SELECT * FROM {table} WHERE Name LIKE @value";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);
            command.Parameters.AddWithValue("@value", '%' + searchTerm + '%');

            Console.WriteLine("Statement: " );

            string finalSql = command.CommandText;
            foreach (SqlParameter param in command.Parameters)
            {
                finalSql = finalSql.Replace(param.ParameterName, param.Value.ToString());
            }

            // Write the completed command statement to the console
            Console.WriteLine("Statement: " + finalSql);

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    ProductModel product = new ProductModel { ID = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3], Stock = (int)reader[4] };
                    product.Image = reader[5] == DBNull.Value ? null : (byte[])reader[5];
                    foundProducts.Add(product);
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        return foundProducts;
    }

    public bool Insert(ProductModel product)
    {
        bool success = false;

        string sqlStatement = $"INSERT INTO {table} (Name, Price, Description, Stock, Image) VALUES (@Name, @Price, @Description, @Stock, @Image)";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Image", product.Image);

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                success = true;

                Console.WriteLine("Success");

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        return success;
    }

    public bool UpdateByID(int ID, ProductColumns column, object value)
    {
        bool success = false;

        string sqlStatement = $"UPDATE {table} SET {column} = @value WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);
            command.Parameters.AddWithValue("@value", value);
            command.Parameters.AddWithValue("@ID", ID);

            Console.WriteLine("Statement: " );

            string finalSql = command.CommandText;
            foreach (SqlParameter param in command.Parameters)
            {
                finalSql = finalSql.Replace(param.ParameterName, param.Value.ToString());
            }

            // Write the completed command statement to the console
            Console.WriteLine("Statement: " + finalSql);

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                success = true;

                Console.WriteLine("Success");

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        return success;
    }


    string[] productColumns = {"Name", "Price", "Description", "Stock", "Image"};
    public bool Update(ProductModel updatedProduct) {
        bool success = false;
        ProductModel existingProduct = GetProductByID(updatedProduct.ID);


        List<string> columnsToUpdate = new List<string>();
        List<object> values = new List<object>();

        if(existingProduct.Name != updatedProduct.Name) {
            columnsToUpdate.Add("Name");
            values.Add(updatedProduct.Name);
        }
        if(existingProduct.Price != updatedProduct.Price) {
            columnsToUpdate.Add("Price");
            values.Add(updatedProduct.Price);
        }
        if(existingProduct.Description != updatedProduct.Description) {
            columnsToUpdate.Add("Description");
            values.Add(updatedProduct.Description);
        }
        if(existingProduct.Stock != updatedProduct.Stock) {
            columnsToUpdate.Add("Stock");
            values.Add(updatedProduct.Stock);
        }
        if(updatedProduct.Image != null) {
            if(existingProduct.Image != updatedProduct.Image) {
                columnsToUpdate.Add("Image");
                values.Add(updatedProduct.Image);
            }
        }


        
        if(columnsToUpdate.Count == 0) return true;

        string sqlStatementColumns = "";
        for(int i=0; i < columnsToUpdate.Count; i++) {
            sqlStatementColumns += " " + columnsToUpdate[i] + " = @" + columnsToUpdate[i];
            if (i != columnsToUpdate.Count - 1) {
                sqlStatementColumns += ", ";
            }
        }


        string sqlStatement = $"UPDATE {table} SET " + sqlStatementColumns + " WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(sqlStatement, connection);
            command.Parameters.AddWithValue("@ID", existingProduct.ID);

            for (int i = 0; i < columnsToUpdate.Count; i++) {
                command.Parameters.AddWithValue("@" + columnsToUpdate[i], values[i]);
            }

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                success = true;

                Console.WriteLine("Success");

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        return success;
    }

    public bool Update(ProductModel product, ProductColumns column, object value) {
        return UpdateByID(product.ID, column, value);
    }

    public int DecreaseStock(ProductModel product) {
        Console.WriteLine("Checking out... " + product.ID);
        int newStock = GetProductByID(product.ID).Stock;
        if(newStock > 0) {
            newStock--;
            Console.WriteLine("Setting stock as.. " + newStock);
            Update(product, ProductColumns.Stock, newStock);
            return newStock;
        }
        else {
            return 0;
        }
    }

    public void SetStock(ProductModel product, int stock) {
        Update(product, ProductColumns.Stock, stock);
    }
}

public enum ProductColumns {
    ID,
    Name,
    Price,
    Description,
    Stock
}