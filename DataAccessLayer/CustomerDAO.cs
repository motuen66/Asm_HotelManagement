using BusinessObject;
using BusinessObject.Enums;
using BusinessObject.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        //private static string SELECT_ALL_CUSTOMERS =    "SELECT * FROM [dbo].[Customer]";
        //private static string INSERT_NEW_CUSTOMERS =    "INSERT [dbo].[Customer] ([CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password])\n" +
        //                                                "VALUES (@FullName, @Telephone, @Email, CAST(@Birthday AS Date), @Status, @Password)";
        //private static string UPDATE_CUSTOMER_BY_ID =   "UPDATE [dbo].[Customer]\n" +
        //                                                "SET\n" +
        //                                                "   CustomerFullName = @FullName,\n" +
        //                                                "   Telephone = @Telephone,\n" +
        //                                                "   EmailAddress = @Email,\n" +
        //                                                "   CustomerBirthday = CAST(@Birthday AS Date),\n" +
        //                                                "   CustomerStatus = @Status,\n" +
        //                                                "   Password = @Password\n" +
        //                                                "WHERE\n    " +
        //                                                "   CustomerID = @CustomerID; ";
        //private static string DELETE_CUSTOMER_BY_ID =   "DELETE FROM [dbo].[Customer]\n" +
        //                                                "WHERE CustomerID = @CustomerID;";
        //private static string SELECT_CUSTOMER_BY_ID =   "SELECT * FROM [dbo].[Customer]" +
        //                                                "WHERE CustomerID = @CustomerID;";

        public static List<Customer> GetAllCustomer()
        {
            using var dbContext = new FuminiHotelManagementContext();

            List<Customer> customerList = dbContext.Customers.ToList();
            return customerList;
        }

        public static void CreateNewCustomer(Customer customer)
        {
            using (var context = new FuminiHotelManagementContext()) { 
                context.Customers.Add(customer);
                int rows = context.SaveChanges();
                Console.WriteLine($"Added {rows} customer into system");
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using var context = new FuminiHotelManagementContext();
            var cus = (from c in context.Customers
                      where c.CustomerId == customer.CustomerId
                      select c).FirstOrDefault();
            if(cus != null)
            {
                cus.CustomerFullName = customer.CustomerFullName; ;
                cus.Telephone = customer.Telephone;
                cus.EmailAddress = customer.EmailAddress;
                cus.CustomerStatus = customer.CustomerStatus;
                int rows = context.SaveChanges();
                Console.WriteLine($"Updated {rows} customer.");
            }
            //DBContext.ExecuteNonQuerry(UPDATE_CUSTOMER_BY_ID, parameters);
        }

        public static void DeleteCustomer(int CustomerId)
        {
            using var context = new FuminiHotelManagementContext();
            Customer cus = (from c in context.Customers
                            where c.CustomerId == CustomerId
                            select c).FirstOrDefault();
            if(cus != null)
            {
                context.Remove(cus);
                int rows = context.SaveChanges();
                Console.WriteLine($"Delelte {rows} customer");
            }
        }

        public static Customer GetCustomer(int customerId) 
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
