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

        public Dictionary<string, float> GetValidDiscounts()
        {
            Dictionary<string, float> discounts = new Dictionary<string, float>();

            foreach(KeyValuePair<Product, int> product in products)
            {
                if(product.Key.code == "RF1" && product.Value > 1)
                {
                    float redFlowersDiscounted = (int)(product.Value / 2);
                    float redFlowerDiscount = redFlowersDiscounted * (product.Key.price / 2);
                    discounts.Add("BOGO50% Red Flowers", redFlowerDiscount);
                    break;
                }
            }

            return discounts;
        }

        public float GetShippingFee()
        {
            float shipping = 0.00f;
            float subtotal = GetSubtotal();

            foreach(KeyValuePair<string, float> discount in GetValidDiscounts())
                subtotal -= discount.Value;

            if (subtotal < 50)
                shipping = 4.95f;
            else if (subtotal < 90)
                shipping = 2.95f;

            return shipping;
        }

        public float GetSubtotal()
        {
            float subtotal = 0;

            foreach (KeyValuePair<Product, int> product in products)
                subtotal += product.Key.price * product.Value;

            return subtotal;
        }

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

        public List<List<string>> GetCartPriceInTableFormat()
        {
            List<List<string>> contents = new List<List<string>>();

            List<string> subtotalEntry = new List<string>();
            subtotalEntry.Add("Subtotal");
            subtotalEntry.Add(GetSubtotal().ToString("0.00"));
            contents.Add(subtotalEntry);

            List<string> discountEntry = new List<string>();
            Dictionary<string, float> discounts = GetValidDiscounts();
            foreach(KeyValuePair<string, float> discount in discounts)
            {
                discountEntry.Add(discount.Key);
                discountEntry.Add(discount.Value.ToString("0.00"));
                contents.Add(discountEntry);
            }

            List<string> shippingEntry = new List<string>();
            shippingEntry.Add("Shipping Fee");
            shippingEntry.Add(GetShippingFee().ToString("0.00"));
            contents.Add(shippingEntry);

            List<string> totalEntry = new List<string>();
            totalEntry.Add("Total");
            float total = System.MathF.Truncate(GetTotal() * 100) / 100;
            totalEntry.Add(total.ToString());
            contents.Add(totalEntry);

            return contents;
        }
    }
}
