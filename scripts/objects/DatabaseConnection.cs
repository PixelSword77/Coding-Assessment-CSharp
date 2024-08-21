using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Assessment_CSharp.scripts.objects
{
    internal class DatabaseConnection
    {
        /// <summary>
        /// An object used to create and manage the connection of the client to a connected database. The ServerName, UserID, Password, and DatabaseName must be specified to make connection.
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <param name="DatabaseName"></param>
        public DatabaseConnection(string ServerName, string UserID, string Password, string DatabaseName)
        {
            server = ServerName;
            user = UserID;
            password = Password;
            database = DatabaseName;
        }

        string server = "";
        string user = "";
        string password = "";
        string database = "";
        MySqlConnection connection;

        /// <summary>
        /// Attempts to connect a connection with the connection info used when creating this connection. Will return true if the connection is successful, and false otherwise, and provides console output based on progress and results.
        /// </summary>
        /// <returns></returns>
        public bool ConnectToDatabase()
        {
            bool connected = false;

            // Create the string required to make connection with MySqlConnection using the established info
            string connectionString = "Server=" + server + ";User ID=" + user + ";Password=" + password + ";Database=" + database;

            // Begin attempting to connect to the database
            MySqlConnection newConnection = new MySqlConnection(connectionString);
            newConnection.Open();
            Console.WriteLine("Attempting new connection to database: " + database);

            // If the connection takes time to connect, a message displays letting the user know that the connection is still attempting before timing out
            int dotCount = 0;
            int connectionTime = 0;
            while (newConnection.State == ConnectionState.Connecting)
            {
                Console.WriteLine("\rTrying to connect" + new string('.', dotCount));
                Task.Delay(10000);

                dotCount++;
                if (dotCount > 3)
                    dotCount = 0;

                connectionTime++;
                if (connectionTime > 1000)
                    break;
            }

            // Outputs the result of the connection, and only marks the connection as successful when the connection opens
            switch (newConnection.State)
            {
                case ConnectionState.Open:
                    Console.WriteLine("Successfully connected to database!");
                    connected = true;
                    break;
                case ConnectionState.Closed:
                    Console.WriteLine("Connection to database closed.");
                    break;
                case ConnectionState.Connecting:
                    Console.WriteLine("Connection took too long and timed out.");
                    break;
            }

            connection = newConnection;
            return connected;
        }

        /// <summary>
        /// Uses the provided query as an SQL query on the connected database, and output the data in a format valid to create a Table object.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<List<string>> GetTableData(string query)
        {
            List<List<string>> lines = new List<List<string>>();

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                List<string> columns = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader.GetValue(i).ToString());
                    columns.Add(reader.GetValue(i).ToString());
                }
                lines.Add(columns);
            }

            return lines;
        }
    }
}
