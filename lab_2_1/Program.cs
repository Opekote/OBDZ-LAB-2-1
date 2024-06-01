using dotenv.net;
using System;

namespace lab_2_1;

class Program
{
    static void Main()
    {
        // Commented because of a weird error caused dotenv.net.Host can’t be null error (just stopped work after pc reload).Maybe a lag due to a electricity cut
        // DotEnv.Load();
        //
        // string host = Environment.GetEnvironmentVariable("DB_HOST");
        // string username = Environment.GetEnvironmentVariable("DB_USERNAME");
        // string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        // string database = Environment.GetEnvironmentVariable("DB_DATABASE");

        // string connectionString = $"Host={host};Username={username};Password={password};Database={database}";
        
        string connectionString = $"Host=localhost;Username=postgres;Password=password;Database=post_service";

        DatabaseHelper databaseHelper = new DatabaseHelper(connectionString);

        CustomerService customerService = new CustomerService(databaseHelper);
        ShipmentService shipmentService = new ShipmentService(databaseHelper);
        PackageService packageService = new PackageService(databaseHelper);
        ReviewService reviewService = new ReviewService(databaseHelper);
        TransactionService transactionService = new TransactionService(databaseHelper);
        ServiceTableService serviceTableService = new ServiceTableService(databaseHelper);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Customers");
            Console.WriteLine("2. Shipments");
            Console.WriteLine("3. Packages");
            Console.WriteLine("4. Reviews");
            Console.WriteLine("5. Transactions");
            Console.WriteLine("6. Services");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CustomerSubMenu(customerService);
                    break;
                case "2":
                    ShipmentSubMenu(shipmentService);
                    break;
                case "3":
                    PackageSubMenu(packageService);
                    break;
                case "4":
                    ReviewSubMenu(reviewService);
                    break;
                case "5":
                    TransactionSubMenu(transactionService);
                    break;
                case "6":
                    ServiceSubMenu(serviceTableService);
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void CustomerSubMenu(CustomerService customerService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Customer Menu:");
            Console.WriteLine("1. Display Customers");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    customerService.DisplayCustomers();
                    break;
                case "2":
                    Console.Write("Enter first name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter last name: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter phone: ");
                    string phone = Console.ReadLine();
                    customerService.AddCustomer(firstName, lastName, email, phone);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void ShipmentSubMenu(ShipmentService shipmentService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Shipment Menu:");
            Console.WriteLine("1. Display Shipments");
            Console.WriteLine("2. Create Shipment");
            Console.WriteLine("3. Display Shipments With Customers");
            Console.WriteLine("4. Display Shipments By Status");
            Console.WriteLine("5. Display Shipment Count");
            Console.WriteLine("6. Display Average Shipment Weight");
            Console.WriteLine("7. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    shipmentService.DisplayShipments();
                    break;
                case "2":
                    Console.Write("Enter tracking number: ");
                    string trackingNumber = Console.ReadLine();
                    Console.Write("Enter sender ID: ");
                    int senderId = int.Parse(Console.ReadLine());
                    Console.Write("Enter recipient ID: ");
                    int recipientId = int.Parse(Console.ReadLine());
                    Console.Write("Enter weight: ");
                    decimal weight = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter dimensions: ");
                    string dimensions = Console.ReadLine();
                    Console.Write("Enter shipment date (YYYY-MM-DD): ");
                    string shipmentDate = Console.ReadLine();
                    Console.Write("Enter delivery date (optional, leave blank if unknown): ");
                    string deliveryDate = Console.ReadLine();
                    Console.Write("Enter status: ");
                    string status = Console.ReadLine();
                    Console.Write("Enter sender address: ");
                    string senderAddress = Console.ReadLine();
                    Console.Write("Enter recipient address: ");
                    string recipientAddress = Console.ReadLine();

                    shipmentService.CreateShipment(trackingNumber, senderId, recipientId, weight, dimensions, shipmentDate, deliveryDate, status, senderAddress, recipientAddress);

                    break;
                case "3":
                    shipmentService.DisplayShipmentsWithCustomers();
                    break;
                case "4":
                    Console.Write("Enter shipment status: ");
                    string statusToFind = Console.ReadLine();
                    shipmentService.DisplayShipmentsByStatus(statusToFind);
                    break;
                case "5":
                    shipmentService.DisplayShipmentCount();
                    break;
                case "6":
                    shipmentService.DisplayAverageShipmentWeight();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void PackageSubMenu(PackageService packageService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Package Menu:");
            Console.WriteLine("1. Display Packages");
            Console.WriteLine("2. Add Package");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    packageService.DisplayPackages();
                    break;
                case "2":
                    Console.Write("Enter shipment ID: ");
                    int shipmentId = int.Parse(Console.ReadLine());
                    Console.Write("Enter package type: ");
                    string packageType = Console.ReadLine();
                    Console.Write("Enter content description: ");
                    string contentDescription = Console.ReadLine();
                    Console.Write("Enter value: ");
                    decimal value = decimal.Parse(Console.ReadLine());
                    packageService.AddPackage(shipmentId, packageType, contentDescription, value);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void ReviewSubMenu(ReviewService reviewService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Review Menu:");
            Console.WriteLine("1. Display Reviews");
            Console.WriteLine("2. Add Review");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    reviewService.DisplayReviews();
                    break;
                case "2":
                    Console.Write("Enter customer ID: ");
                    int customerId = int.Parse(Console.ReadLine());
                    Console.Write("Enter shipment ID: ");
                    int shipmentId = int.Parse(Console.ReadLine());
                    Console.Write("Enter rating (1-5): ");
                    int rating = int.Parse(Console.ReadLine());
                    Console.Write("Enter comment: ");
                    string comment = Console.ReadLine();
                    Console.Write("Enter review date (YYYY-MM-DD): ");
                    DateTime reviewDate = DateTime.Parse(Console.ReadLine());
                    reviewService.AddReview(customerId, shipmentId, rating, comment, reviewDate);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void TransactionSubMenu(TransactionService transactionService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Transaction Menu:");
            Console.WriteLine("1. Display Transactions");
            Console.WriteLine("2. Add Transaction");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    transactionService.DisplayTransactions();
                    break;
                case "2":
                    Console.Write("Enter shipment ID: ");
                    int shipmentId = int.Parse(Console.ReadLine());
                    Console.Write("Enter service ID: ");
                    int serviceId = int.Parse(Console.ReadLine());
                    Console.Write("Enter transaction date (YYYY-MM-DD): ");
                    DateTime transactionDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    transactionService.AddTransaction(shipmentId, serviceId, transactionDate, amount);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void ServiceSubMenu(ServiceTableService serviceTableService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Service Menu:");
            Console.WriteLine("1. Display Services");
            Console.WriteLine("2. Add Service");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    serviceTableService.DisplayServices();
                    break;
                case "2":
                    Console.Write("Enter service name: ");
                    string serviceName = Console.ReadLine();
                    Console.Write("Enter description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter price: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    serviceTableService.AddService(serviceName, description, price);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }
    
    
}
