using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class CustomersModel : PageModel
    {
        // create a list to hold list of customers we retrieve
        public List<Customer> Customers {get; set;}

        public void OnGet()
        {
            // intialize the list of customers
            Customers = new List<Customer>();

            // set the string with database connection info
            string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
            
            // open database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // open db connection
                connection.Open();

                // create a string to hold the sql statement we want to execute
                string sql = "SELECT CustomerID, CompanyName, ContactName, Country FROM Customers";
                
                // run the SQL statement against the db
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // create a new data reader to read the records
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // loop through the result set - records we got from db
                        while (reader.Read())
                        {
                            // create a new customer object
                            Customers.Add(new Customer
                            {
                               CustomerID = reader.GetString(0),
                               CompanyName = reader.GetString(1),
                               ContactName = reader.GetString(2),
                               Country = reader.GetString(3)
                            });
                        }
                    }
                }
            }
        }
    }
}

// class for representing a customer from Nortwind database
public class Customer
{
    public string CustomerID {get; set;}
    public string CompanyName {get; set;}
    public string ContactName {get; set;}
    public string Country {get; set;}
}