using Npgsql;

namespace lab_2_1;

public class PackageService
{
    private DatabaseHelper _databaseHelper;

    public PackageService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayPackages()
    {
        string query = "SELECT * FROM Package";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["PackageID"]}, {reader["ShipmentID"]}, {reader["PackageType"]}, {reader["ContentDescription"]}, {reader["Value"]}");
                }
            }
        }
    }

    public void AddPackage(int shipmentId, string packageType, string contentDescription, decimal value)
    {
        string query = "INSERT INTO Package (ShipmentID, PackageType, ContentDescription, Value) VALUES (@shipmentId, @packageType, @contentDescription, @value)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("shipmentId", shipmentId);
                command.Parameters.AddWithValue("packageType", packageType);
                command.Parameters.AddWithValue("contentDescription", contentDescription);
                command.Parameters.AddWithValue("value", value);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Package added.");
    }
}