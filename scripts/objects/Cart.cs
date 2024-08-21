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

        public void AddProduct(Product product, int quantity)
        {
            if (products.ContainsKey(product))
                products[product] += quantity;
            else
                products.Add(product, quantity);

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

        public void ClearCart()
        {
            products.Clear();
            Console.WriteLine("-- You cart has been cleared! --");
        }

        public float GetSubtotal()
        {
            float subtotal = 0;

            foreach (KeyValuePair<Product, int> product in products)
            {
                subtotal += product.Key.price * product.Value;
            }

            return subtotal;
        }

        public List<List<string>> GetCartContentsInTableFormat()
        {
            List<List<string>> contents = new List<List<string>>();

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
    }
}
