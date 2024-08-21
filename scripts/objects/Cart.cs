using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class Cart
    {
        Dictionary<Product, int> products = new Dictionary<Product, int>();

        /// <summary>
        /// Adds a product to the cart as a new dictionary entry if the product is not already in the cart. If the product is in the cart, increment the existing quantity by the input quantity. Removes the product from the cart if the quantity goes to or below 0.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddProduct(Product product, int quantity)
        {
            // If the product already exists in the cart, we need to add the quantity entered to the existing dictionary entry
            if (products.ContainsKey(product))
                products[product] += quantity;
            else
                products.Add(product, quantity);

            // If we reduce the products quantity to or below 0, we need to remove it from the cart to not affect price calculations
            if (products[product] <= 0)
            {
                products.Remove(product);
                Console.WriteLine("-- Removed " + quantity + " from your cart! --");
            }
            else
            {
                Console.WriteLine("-- Added " + quantity + " " + product.name + "s to your cart! --");
            }
        }

        /// <summary>
        /// Remove all dictionary entries from the cart.
        /// </summary>
        public void ClearCart()
        {
            products.Clear();
            Console.WriteLine("-- You cart has been cleared! --");
        }

        /// <summary>
        /// Checks the contents of the carts against predetermined discounts, such as BOGO50% Red Flowers, and accumulates the discounts in a dictionary containiner the discount name and the total amount saved from the discount
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, float> GetValidDiscounts()
        {
            Dictionary<string, float> discounts = new Dictionary<string, float>();

            // Check if we have Red Flowers in our cart, and if we have 2 or more
            foreach(KeyValuePair<Product, int> product in products)
            {
                if(product.Key.code == "RF1" && product.Value > 1)
                {
                    // Get the number of discounted flowers by truncating the quantity divided by 2 and calculate the discount by the product's price
                    float redFlowersDiscounted = (int)(product.Value / 2);
                    float redFlowerDiscount = redFlowersDiscounted * (product.Key.price / 2);

                    if (discounts.ContainsKey("BOGO50% Red Flowers"))
                        discounts["BOGO50% Red Flowers"] += redFlowerDiscount;
                    else
                        discounts.Add("BOGO50% Red Flowers", redFlowerDiscount);

                    break;
                }
            }

            return discounts;
        }

        /// <summary>
        /// Returns the shipping fee relative to the total price of the cart, which includes all valid discounts.
        /// </summary>
        /// <returns></returns>
        public float GetShippingFee()
        {
            // Initializes the subtotal instead of total, as the total relies on the shipping fee and will infinitely loop
            float shipping = 0.00f;
            float subtotal = GetSubtotal();

            // Manually recudes the subtotal by the valid discounts
            Dictionary<string, float> discounts = GetValidDiscounts();
            foreach (KeyValuePair<string, float> discount in discounts)
            {
                subtotal -= discount.Value;
            }

            // A total of less than $50 costs $4.95 to ship, of less than $90 costs $2.95, and in all other cases is free
            if (subtotal < 50)
                shipping = 4.95f;
            else if (subtotal < 90)
                shipping = 2.95f;

            return shipping;
        }

        /// <summary>
        /// Adds up and returns the subtotal of the cart, not including any shipping fees or discounts.
        /// </summary>
        /// <returns></returns>
        public float GetSubtotal()
        {
            float subtotal = 0;

            // Add up the price of all products multiplies by the quantity in the cart
            foreach (KeyValuePair<Product, int> product in products)
                subtotal += product.Key.price * product.Value;

            return subtotal;
        }

        /// <summary>
        /// Calculates the total price of the cart, including any shipping fees and all discounts.
        /// </summary>
        /// <returns></returns>
        public float GetTotal()
        {
            float total = GetSubtotal();

            Dictionary<string, float> discounts = GetValidDiscounts();
            foreach (KeyValuePair<string, float> discount in discounts)
            {
                total -= discount.Value;
            }

            total += GetShippingFee();

            return total;
        }

        /// <summary>
        /// Converts and outputs the products contained in this cart into a valid dataset, List<List<string>>, to be used in a table object.
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetCartContentsInTableFormat()
        {
            List<List<string>> contents = new List<List<string>>();

            // Convert each product entry in the cart into an entry in the contents variable, displaying its name, quantity, and individual subtotal
            foreach (KeyValuePair<Product, int> product in products)
            {
                List<string> cartListing = new List<string>();
                cartListing.Add(product.Key.name);
                cartListing.Add("x" + product.Value);
                cartListing.Add((product.Key.price * product.Value).ToString("0.00"));
                contents.Add(cartListing);
            }

            return contents;
        }

        /// <summary>
        /// Calculates and outputs all prices affecting the cart, including the subtotal, discounts, shipping, and final total, in a valid dataset, List<List<string>>, to be used in a table object.
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetCartPriceInTableFormat()
        {
            List<List<string>> contents = new List<List<string>>();

            // Adds an entry to the contents output that displays the cart subtotal
            List<string> subtotalEntry = new List<string>();
            subtotalEntry.Add("Subtotal");
            subtotalEntry.Add(GetSubtotal().ToString("0.00"));
            contents.Add(subtotalEntry);

            // If we have valid discounts for this cart, adds an entry to contents that displays the applicable discounts
            List<string> discountEntry = new List<string>();
            Dictionary<string, float> discounts = GetValidDiscounts();
            foreach(KeyValuePair<string, float> discount in discounts)
            {
                discountEntry.Add(discount.Key);
                discountEntry.Add(discount.Value.ToString("0.00"));
                contents.Add(discountEntry);
            }

            // Adds an entry to the contents output that displays the cart's shipping fee 
            List<string> shippingEntry = new List<string>();
            shippingEntry.Add("Shipping Fee");
            shippingEntry.Add(GetShippingFee().ToString("0.00"));
            contents.Add(shippingEntry);

            // Adds an entry to the contents output that displays the cart's final total
            List<string> totalEntry = new List<string>();
            totalEntry.Add("Total");
            float total = System.MathF.Truncate(GetTotal() * 100) / 100;
            totalEntry.Add(total.ToString());
            contents.Add(totalEntry);

            return contents;
        }
    }
}
