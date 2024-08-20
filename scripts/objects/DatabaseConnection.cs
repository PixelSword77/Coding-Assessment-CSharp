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
        public DatabaseConnection(string server, string user, string password, string database)
        {
            ServerName = server;
            UserID = user;
            Password = password;
            DatabaseName = database;
        }

        string ServerName = "";
        string UserID = "";
        string Password = "";
        string DatabaseName = "";
        MySqlConnection connection;

        public bool ConnectToDatabase()
        {
            bool connected = false;

            string connectionString = "Server=" + ServerName + ";User ID=" + UserID + ";Password=" + Password + ";Database=" + DatabaseName;

            MySqlConnection newConnection = new MySqlConnection(connectionString);
            newConnection.Open();
            Console.WriteLine("Attempting new connection to database: " + DatabaseName);

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

            return connected;
        }
    }
}
