using Coding_Assessment_CSharp.scripts.objects;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("#############################");
        Console.WriteLine("Initializing...");

        // Initialize the connection to the database, exit if the connection fails
        DatabaseConnection dbConnection = new DatabaseConnection("localhost", "username", "password", "products");
        if(!dbConnection.ConnectToDatabase())
        {
            Console.WriteLine("Database connection failed, exiting program.");
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }

        // Retrieve the list of products and store them in an object
        List<List<string>> tableData = dbConnection.GetTableData("select * from products_list");
        foreach (List<string> table in tableData)
        {
            Console.WriteLine(table[0] + " - " + table[1] + " - " + table[2]);
        }

        // Print a table to the console showing the available products, codes, and prices

        // Get user's input for the product code and quantity to add to cart

        // Add the user's input to a cart

        // Display the cart on the screen

        // Calculate the amount saved from discounts (buy on red flower, get one half off)

        // Calculate shipping cost

        // Calculate total

        // Display total

        Console.ReadKey();
    }
}