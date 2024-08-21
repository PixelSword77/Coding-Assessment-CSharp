using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class ProductTable : Table
    {
        /// <summary>
        /// Returns the same value as CreateTableString, but uses a dataset from a list of Products for simplicity.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public string CreateTableStringFromProducts(string header, List<Product> products)
        {
            List<List<string>> productsList = new List<List<string>>();

            // Add each products in our product list to the new dataset to be converted into a printable table
            foreach (Product product in products)
            {
                List<string> newProductListing = new List<string>();
                newProductListing.Add(product.name);
                newProductListing.Add(product.code);
                newProductListing.Add(product.price.ToString());
                productsList.Add(newProductListing);
            }

            return CreateTableString(header, productsList);
        }
    }
}
