using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class Product
    {
        public Product(string Name, string Code, float Price)
        {
            name = Name;
            code = Code;
            price = Price;
        }

        public string name = "";
        public string code = "";
        public float price = 0.00f;
    }
}
