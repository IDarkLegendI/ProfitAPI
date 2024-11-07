using MySqlConnector;

namespace ProfitAPI;

public static class Database
{
    public static void CheckDatabaseConnection()
    {
        using (var connection = new MySqlConnection("Server=my-mysql;Port=3306;Database=products;User=root;Password=example;"))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    } 
}