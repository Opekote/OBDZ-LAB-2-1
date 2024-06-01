using Npgsql;

namespace lab_2_1;

public class ShipmentService
{
    private readonly DatabaseHelper _databaseHelper;

    public ShipmentService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayShipments()
    {
        var query = "SELECT * FROM Shipment";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    Console.WriteLine(
                        $"{reader["ShipmentID"]}, {reader["TrackingNumber"]}, {reader["Weight"]}, {reader["Dimensions"]}, {reader["Status"]}");
            }
        }
    }
    

    public void DisplayShipmentsWithCustomers()
    {
        var query = @"
            SELECT s.TrackingNumber, s.Weight, c.FirstName AS SenderFirstName, c.LastName AS SenderLastName, c2.FirstName AS RecipientFirstName, c2.LastName AS RecipientLastName
            FROM Shipment s
            JOIN Customer c ON s.SenderID = c.CustomerID
            JOIN Customer c2 ON s.RecipientID = c2.CustomerID";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    Console.WriteLine(
                        $"{reader["TrackingNumber"]}, {reader["Weight"]}, Sender: {reader["SenderFirstName"]} {reader["SenderLastName"]}, Recipient: {reader["RecipientFirstName"]} {reader["RecipientLastName"]}");
            }
        }
    }

    public void DisplayShipmentsByStatus(string status)
    {
        var query = "SELECT * FROM Shipment WHERE Status = @status";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("status", status);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        Console.WriteLine($"{reader["ShipmentID"]}, {reader["TrackingNumber"]}, {reader["Status"]}");
                }
            }
        }
    }

    public void DisplayShipmentCount()
    {
        var query = "SELECT COUNT(*) AS ShipmentCount FROM Shipment";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read()) Console.WriteLine($"Total shipments: {reader["ShipmentCount"]}");
            }
        }
    }

    public void DisplayAverageShipmentWeight()
    {
        var query = "SELECT AVG(Weight) AS AverageWeight FROM Shipment";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read()) Console.WriteLine($"Average shipment weight: {reader["AverageWeight"]}");
            }
        }
    }

    public void CreateShipment(string trackingNumber, int senderId, int recipientId, decimal weight, string dimensions, string shipmentDate, string deliveryDate, string status, string senderAddress, string recipientAddress)
    {
        var query = @"
        INSERT INTO Shipment (TrackingNumber, SenderID, RecipientID, Weight, Dimensions, ShipmentDate, DeliveryDate, Status, SenderAddress, RecipientAddress)
        VALUES (@trackingNumber, @senderId, @recipientId, @weight, @dimensions, @shipmentDate::DATE, @deliveryDate::DATE, @status, @senderAddress, @recipientAddress)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("trackingNumber", trackingNumber);
                command.Parameters.AddWithValue("senderId", senderId);
                command.Parameters.AddWithValue("recipientId", recipientId);
                command.Parameters.AddWithValue("weight", weight);
                command.Parameters.AddWithValue("dimensions", dimensions);
                command.Parameters.AddWithValue("shipmentDate", shipmentDate);
                command.Parameters.AddWithValue("deliveryDate", string.IsNullOrEmpty(deliveryDate) ? DBNull.Value : (object)deliveryDate);
                command.Parameters.AddWithValue("status", status);
                command.Parameters.AddWithValue("senderAddress", senderAddress);
                command.Parameters.AddWithValue("recipientAddress", recipientAddress);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Shipment created successfully.");
    }

}
