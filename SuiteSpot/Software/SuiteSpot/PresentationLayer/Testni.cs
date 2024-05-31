using System;
using System.Diagnostics;
using System.Linq;
using DataAccessLayer; // Make sure the namespace is correct

namespace PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDatabaseConnection();
        }

        static void TestDatabaseConnection()
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    // Attempt to open the connection
                    context.Database.Connection.Open();
                    Debug.WriteLine("Connection opened successfully.");
                    Console.WriteLine("Connection opened successfully."); // Optional: Also print to console

                    // Simple query to test the connection
                    if (context.Database.Connection.State == System.Data.ConnectionState.Open)
                    {
                        var query = context.Room.Count(); // Simple query to test the connection
                        Debug.WriteLine("Connection successful. Number of rooms: " + query);
                        Console.WriteLine("Connection successful. Number of rooms: " + query); // Optional: Also print to console
                    }
                    else
                    {
                        Debug.WriteLine("Connection failed to remain open.");
                        Console.WriteLine("Connection failed to remain open."); // Optional: Also print to console
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        static void LogException(Exception ex)
        {
            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Debug.WriteLine("Connection failed: " + ex.Message);
            Console.WriteLine("Connection failed: " + ex.Message); // Optional: Also print to console

            if (ex.InnerException != null)
            {
                Debug.WriteLine("Inner Exception: " + ex.InnerException.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message); // Optional: Also print to console
            }
        }
    }
}
