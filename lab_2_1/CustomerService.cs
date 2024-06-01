using Npgsql;

namespace lab_2_1;

public class CustomerService
{
    private readonly DatabaseHelper _databaseHelper;

    public CustomerService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayCustomers()
    {
        var query = "SELECT * FROM Customer";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    Console.WriteLine(
                        $"{reader["CustomerID"]}, {reader["FirstName"]}, {reader["LastName"]}, {reader["Email"]}, {reader["Phone"]}");
            }
        }
    }

    public void AddCustomer(string firstName, string lastName, string email, string phone)
    {
        var query =
            "INSERT INTO Customer (FirstName, LastName, Email, Phone) VALUES (@firstName, @lastName, @email, @phone)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("firstName", firstName);
                command.Parameters.AddWithValue("lastName", lastName);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("phone", phone);
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Customer added.");
    }
}