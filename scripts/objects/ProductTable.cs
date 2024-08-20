using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class ProductTable
    {
        public string CreateProductsTable(List<Product> products)
        {
            string output = "";

            int nameExtraPadding = 5;
            int codeExtraPadding = 5;
            int priceExtraPadding = 5;

            int maxNameLength = 0;
            int maxCodeLength = 0;
            int maxPriceLength = 0;
            foreach (Product product in products)
            {
                if (product.name.Length > maxNameLength)
                    maxNameLength = product.name.Length;
                if (product.code.Length > maxCodeLength)
                    maxCodeLength = product.code.Length;
                if (product.price.ToString().Length > maxPriceLength)
                    maxPriceLength = product.price.ToString().Length;
            }

            output += ("# Product " + new string('#', maxNameLength + nameExtraPadding - 3) + " Code " + new string('#', maxPriceLength + priceExtraPadding - 4) + " Price #\n");
            for (int i = 0; i < products.Count; i++)
            {
                int namePadding = maxNameLength - products[i].name.Length;
                int codePadding = maxCodeLength - products[i].code.Length;
                int pricePadding = maxPriceLength - products[i].price.ToString().Length;
                output += ("# " + products[i].name + new string(' ', namePadding + nameExtraPadding) + "| "
                    + new string(' ', codePadding + codeExtraPadding) + products[i].code + " | "
                    + new string(' ', pricePadding + priceExtraPadding) + products[i].price + " #\n");
            }
            output += (new String('#', maxNameLength + maxCodeLength + maxPriceLength + nameExtraPadding + codeExtraPadding + priceExtraPadding + 9));

            return output;
        }
    }
}
