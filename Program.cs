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

        //Create a new cart to be used during the shopping process
        Cart cart = new Cart();

        // Process the user's shopping
        while (true)
        {
            // Print a table to the console showing the available products, codes, and prices
            Table productTable = new Table();
            Console.WriteLine(productTable.CreateTableStringFromProducts("# Products", products, 3));
            Console.WriteLine("");

            // If we have items in our cart, print a table showing its contents
            if (cart.GetSubtotal() > 0)
            {
                Table cartTable = new Table();
                Console.WriteLine(productTable.CreateTableString("# Cart", cart.GetCartContentsInTableFormat(), 3));
                Console.WriteLine("");
            }

            // Get user's input for the product code
            Console.WriteLine("Enter the product code you'd like to add to your cart, or enter 'CLEAR' to empty your cart:");
            string productCode = Console.ReadLine();
            Product selectedProduct = null;

            //Clear the cart if "CLEAR" is entered
            if (productCode == "CLEAR")
            {
                cart.ClearCart();
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            // Validate the user input a valid product code
            bool inputValidated = false;
            foreach (Product product in products)
            {
                if (product.code == productCode)
                {
                    inputValidated = true;
                    selectedProduct = product;
                }
            }

            if(!inputValidated)
            {
                Console.WriteLine("Invalid product code, press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            // Get the user's input for the quantity of the prior input product code
            Console.WriteLine("Enter the quantity you'd like to add to your cart:");
            int quantity = 0;
            inputValidated = false;

            //Validate the user entered a valid integer
            inputValidated = int.TryParse(Console.ReadLine(), out quantity);
            if(!inputValidated)
            {
                Console.WriteLine("Invalid quantity, press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            // Add the user's input to a cart
            if(selectedProduct != null)
            {
                cart.AddProduct(selectedProduct, quantity);
            }

            // Display the cart on the screen

            // Calculate the amount saved from discounts (buy on red flower, get one half off)

            // Calculate shipping cost

            // Calculate total

            // Display total

            Console.ReadKey();
            Console.Clear();
        }
    }
}