using Npgsql;

namespace lab_2_1;

public class TransactionService
{
    private DatabaseHelper _databaseHelper;

    public TransactionService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayTransactions()
    {
        string query = "SELECT * FROM Transaction";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["TransactionID"]}, {reader["ShipmentID"]}, {reader["ServiceID"]}, {reader["TransactionDate"]}, {reader["Amount"]}");
                }
            }
        }
    }

    public void AddTransaction(int shipmentId, int serviceId, DateTime transactionDate, decimal amount)
    {
        string query = "INSERT INTO Transaction (ShipmentID, ServiceID, TransactionDate, Amount) VALUES (@shipmentId, @serviceId, @transactionDate::DATE, @amount)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("shipmentId", shipmentId);
                command.Parameters.AddWithValue("serviceId", serviceId);
                command.Parameters.AddWithValue("transactionDate", transactionDate);
                command.Parameters.AddWithValue("amount", amount);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Transaction added.");
    }
}