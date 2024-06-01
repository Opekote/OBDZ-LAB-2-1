using Npgsql;

namespace lab_2_1;

public class ReviewService
{
    private DatabaseHelper _databaseHelper;

    public ReviewService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public void DisplayReviews()
    {
        string query = "SELECT * FROM Review";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ReviewID"]}, {reader["CustomerID"]}, {reader["ShipmentID"]}, {reader["Rating"]}, {reader["Comment"]}, {reader["ReviewDate"]}");
                }
            }
        }
    }

    public void AddReview(int customerId, int shipmentId, int rating, string comment, DateTime reviewDate)
    {
        string query = "INSERT INTO Review (CustomerID, ShipmentID, Rating, Comment, ReviewDate) VALUES (@customerId, @shipmentId, @rating, @comment, @reviewDate::DATE)";
        using (var connection = _databaseHelper.GetConnection())
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("customerId", customerId);
                command.Parameters.AddWithValue("shipmentId", shipmentId);
                command.Parameters.AddWithValue("rating", rating);
                command.Parameters.AddWithValue("comment", comment);
                command.Parameters.AddWithValue("reviewDate", reviewDate);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Review added.");
    }
}