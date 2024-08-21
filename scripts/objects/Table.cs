using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class Table
    {
        int padding = 3;

        /// <summary>
        /// A visual representation of data, specifically in the format of List<List<string>>.
        /// </summary>
        public Table()
        {
            
        }

        /// <summary>
        /// A visual representation of data, specifically in the format of List<List<string>>. Sets the padding of the columns.
        /// </summary>
        public Table(int TablePadding)
        {
            padding = TablePadding;
        }

        /// <summary>
        /// Generates a printable string from a dataset, formated with a header and borders.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string CreateTableString(string header, List<List<string>> values)
        {
            string output = "";

            // Throws an error if our dataset is empty as an edge case
            if(values.Count == 0)
            {
                Console.WriteLine("ERR: Tried to create table '" + header + "' but the input values list is empty");
                return "";
            }

            // Get the number of columns to know how many values there are per value
            int columns = values[0].Count();

            // Initialize a list of integers used to represent the highest length value in each column, to be used for spacing columns evenly in the output
            List<int> columnLengths = new List<int>();
            for(int x = 0; x < columns; x++)
                columnLengths.Add(0);

            // Find the highest character count value in each column and store the length so that it can be used to calculate column padding
            for (int t = 0; t < columns; t++)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i][t].Length > columnLengths[t])
                        columnLengths[t] = values[i][t].Length;
                }
            }

            // Add each row of values to the table, adding the proper amount of spacing calculated prior and including any extra padded added when intitializing the table
            output += header + "\n";
            output += "---------------------------------------------------------\n";
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
