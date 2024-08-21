using Coding_Assessment_CSharp.scripts.objects;

/// <summary>
/// This program runs in the console, first outputting debug lines during initialization to confirm
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("#############################");
        Console.WriteLine("Initializing...");

        // Initialize the connection to the products database and stores it as a reusable object using abstraction principles to hide the MySqlConnection results and data , which is to be used in the next section, exits if the connection fails
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

        // Create a list of all loaded products from the loaded table data
        List<Product> products = new List<Product>();
        foreach (List<string> loadedProduct in tableData)
        {
            Product newProduct = new Product(loadedProduct[1], loadedProduct[0], float.Parse(loadedProduct[2]));
            products.Add(newProduct);
        }

        // End initialization and clear console. Waits for user input
        Console.WriteLine("Initialization finished successfully, press any key to launch.");
        Console.WriteLine("#############################");
        Console.ReadKey();
        Console.Clear();

        //Create a new cart to be used during the shopping process
        Cart cart = new Cart();

        // Demonstrate one aspect of inheritance and polymorphism by saving the tables in a dictionary containing any table type, which includes ProductTables
        Dictionary<string, Table> drawnTables = new Dictionary<string, Table>();
        drawnTables.Add("Products", new ProductTable());
        drawnTables.Add("Cart", new Table());
        drawnTables.Add("Prices", new Table(3));

        // The main loop to process the user's shopping
        while (true)
        {
            // Print a table to the console showing the available products, codes, and prices
            Console.WriteLine(((ProductTable)drawnTables["Products"]).CreateTableStringFromProducts("# Products", products));
            Console.WriteLine("");

            // If we have items in our cart, print a table showing its contents
            if (cart.GetSubtotal() > 0)
            {
                Console.WriteLine(drawnTables["Cart"].CreateTableString("# Cart", cart.GetCartContentsInTableFormat()));
                Console.WriteLine("");
            }

            // If we have items in our cart, show what our current subtotal and final total is, including applied discounts and shipping
            if (cart.GetSubtotal() > 0)
            {
                Console.WriteLine(drawnTables["Prices"].CreateTableString("# Prices", cart.GetCartPriceInTableFormat()));
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

            // Waits for the user to input so that they can see the results of their input before allowing them to input again
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}