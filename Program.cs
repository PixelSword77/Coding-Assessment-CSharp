using Coding_Assessment_CSharp.scripts.objects;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("#############################");
        Console.WriteLine("Initializing...");

        // Initialize the connection to the products database, exit if the connection fails
        DatabaseConnection dbConnection = new DatabaseConnection("localhost", "username", "password", "products");
        if(!dbConnection.ConnectToDatabase())
        {
            Console.WriteLine("Database connection failed, exiting program.");
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }

        // Retrieve the list of products in the products_list table
        Console.WriteLine("Fetching list of products...");

        List<List<string>> tableData = dbConnection.GetTableData("select * from products_list");
        foreach (List<string> table in tableData)
        {
            Console.WriteLine(table[0] + " - " + table[1] + " - " + table[2]);
        }

        // Create a list of all loaded products from the loaded table data
        List<Product> products = new List<Product>();
        foreach (List<string> loadedProduct in tableData)
        {
            Product newProduct = new Product(loadedProduct[1], loadedProduct[0], float.Parse(loadedProduct[2]));
            products.Add(newProduct);
        }

        // End initialization and clear console
        Console.WriteLine("Initialization finished successfully, press any key to launch.");
        Console.WriteLine("#############################");
        Console.ReadKey();
        Console.Clear();

        // Print a table to the console showing the available products, codes, and prices
        ProductTable productTable = new ProductTable();
        Console.WriteLine(productTable.CreateProductsTable(products));
        Console.WriteLine("");

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