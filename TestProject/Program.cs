using BusinessObject;
using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net.Http.Headers;

namespace TestProject
{
    internal class Program
    {
        public static async Task CreateDatabase()
        {
            using (var dbContext = new FuminiHotelManagementContext()) {
                String databasename = dbContext.Database.GetDbConnection().Database;
                Console.WriteLine("Tạo " + databasename);
                bool res = await dbContext.Database.EnsureCreatedAsync();
                string resultstring = res ? "success" : "already";
                Console.WriteLine($"CSDL {databasename} : {resultstring}");
            }
        }
       //async function
        static async Task InsertCustomerAsync()
        {
            using (var context = new FuminiHotelManagementContext()) {
                await context.Customers.AddAsync(new Customer
                {
                    CustomerFullName = "Nguyen Quoc Anh",
                    Telephone = "0123456789",
                    EmailAddress = "newemail2@gmail.com",
                    CustomerBirthday = new DateOnly(2004, 01, 04),
                    CustomerStatus = 1,
                    Password = "123"
                });
                int rows = await context.SaveChangesAsync();
                Console.WriteLine($"Saved {rows} customer");
            }
        }
        //non async
        static void InsertCustomer()
        {
            using (var context = new FuminiHotelManagementContext())
            {
                context.Customers.Add(new Customer
                {
                    CustomerFullName = "Nguyen Quoc Anh",
                    Telephone = "0123456789",
                    EmailAddress = "newemail3@gmail.com",
                    CustomerBirthday = new DateOnly(2004, 01, 04),
                    CustomerStatus = 1,
                    Password = "123"
                });
                int rows = context.SaveChanges();
                Console.WriteLine($"Saved {rows} customer");
            }
        }

        static void DeleteCustomer(int customerId) {
            using var dbContext = new FuminiHotelManagementContext();
            Customer customer = (from c in dbContext.Customers
                                 where c.CustomerId == customerId
                                 select c).FirstOrDefault();
            if(customer != null)
            {
                dbContext.Remove(customer);
                int rows = dbContext.SaveChanges();
                Console.WriteLine($"Delete {rows} customer");
            }
        }
        static void UpdateCustomer(int id, Customer newInfor) {
            using var dbContext = new FuminiHotelManagementContext();
            var cus = (from c in dbContext.Customers
                      where c.CustomerId == id
                      select c).FirstOrDefault();
            if(cus != null)
            {
                //tracking
                //EntityEntry<Customer> entry = dbContext.Entry(cus);
                //entry.State = EntityState.Detached;

                cus.CustomerFullName = newInfor.CustomerFullName;
                cus.Telephone = newInfor.Telephone;
                cus.EmailAddress = newInfor.EmailAddress;
                cus.CustomerStatus = newInfor.CustomerStatus;
                cus.Password = newInfor.Password;

                int changedRow = dbContext.SaveChanges();
                Console.WriteLine($"Upadated {changedRow} customer row");
            }

        }

        static void ReadCustomers()
        {
            using var dbContext = new FuminiHotelManagementContext();
            //var customers = dbContext.Customers.ToList();
            //var customerVip = new List<Customer>();
            //foreach(var c in customers)
            //{
            //    if(c.CustomerId < 10)
            //    {
            //        customerVip.Add(c);
            //    }
            //}
            //customerVip.ForEach(c => { Console.WriteLine(c.ToString()); });

            var qr = from cus in dbContext.Customers
                      where cus.CustomerId < 10
                      orderby cus.CustomerId descending
                      select cus;           
            qr.ToList().ForEach(cus => { Console.WriteLine(cus.ToString()); });
            //Console.WriteLine(qr.ToString());
        }

        static async Task Main(string[] args)
        {
            //await CreateDatabase();
            ////InsertCustomer();

            ////delete
            //DeleteCustomer(20);

            ////read
            //ReadCustomers();

            //update
            //Customer newCus = new Customer
            //{
            //    CustomerFullName = "Nguyen Ngoc Ngan",
            //    Telephone = "012345678",
            //    EmailAddress = "newemail3@gmail.com",
            //    CustomerBirthday = new DateOnly(2004, 01, 04),
            //    CustomerStatus = 1,
            //    Password = "123"
            //};
            //UpdateCustomer(18, newCus);

            //using var context = new FuminiHotelManagementContext();
            //var bookings = context.BookingReservations.ToList();
            //foreach(BookingReservation br in bookings)
            //{
            //    Console.WriteLine($"{br.BookingReservationId}; {br.TotalPrice}; {br.BookingStatus}");
            //}
            using var context = new FuminiHotelManagementContext();
            //var br = (from bookingReservation in context.BookingReservations
            //          where bookingReservation.BookingReservationId == 1
            //          select bookingReservation).FirstOrDefault();
            //var bds = context.BookingDetails.Where(bd => bd.BookingReservationId == br.BookingReservationId).ToList();
            var br = context.BookingReservations.Include(b => b.BookingDetails).Where(b => b.BookingReservationId == 1).ToList();
            foreach (BookingReservation b in br)
            {
                Console.WriteLine($"{b.BookingReservationId}");
                //Console.WriteLine($"{.}");
                foreach (BookingDetail bd in b.BookingDetails)
                {
                    Console.WriteLine($"{bd.ActualPrice}");
                }
                Console.WriteLine("");
            }
        }
    }
}
