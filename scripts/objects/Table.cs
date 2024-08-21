using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class Table
    {
        public string CreateTableStringFromProducts(string header, List<Product> products, int padding)
        {
            List<List<string>> productsList = new List<List<string>>();

            foreach (Product product in products)
            {
                List<string> newProductListing = new List<string>();
                newProductListing.Add(product.name);
                newProductListing.Add(product.code);
                newProductListing.Add(product.price.ToString());
                productsList.Add(newProductListing);
            }

            return CreateTableString(header, productsList, padding);
        }

        public string CreateTableString(string header, List<List<string>> values, int padding)
        {
            string output = "";

            int columns = values[0].Count();

            List<int> columnLengths = new List<int>();
            for(int x = 0; x < columns; x++)
                columnLengths.Add(0);

            for (int t = 0; t < columns; t++)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i][t].Length > columnLengths[t])
                        columnLengths[t] = values[i][t].Length;
                }
            }

            output += header + "\n";
            output += "------------------------------------------------------------------\n";
            for (int i = 0; i < values.Count; i++)
            {
                output += "#";
                for (int t = 0; t < columns; t++)
                {
                    output += values[i][t] + new string(' ', columnLengths[t] - values[i][t].Length + padding) + "|";
                }
                output += "\n";
            }

            return output;

        }
    }
}
