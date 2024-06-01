using Npgsql;

namespace lab_2_1;

public class ServiceTableService
{
    private DatabaseHelper _databaseHelper;

    public ServiceTableService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayServices()
    {
        string query = "SELECT * FROM Service";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ServiceID"]}, {reader["ServiceName"]}, {reader["Description"]}, {reader["Price"]}");
                }
            }
        }
    }

    public void AddService(string serviceName, string description, decimal price)
    {
        string query = "INSERT INTO Service (ServiceName, Description, Price) VALUES (@serviceName, @description, @price)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("serviceName", serviceName);
                command.Parameters.AddWithValue("description", description);
                command.Parameters.AddWithValue("price", price);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Service added.");
    }
}