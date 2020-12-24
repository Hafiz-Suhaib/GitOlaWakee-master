using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;


namespace OlaWakeel.Services.CustomerService
{
    public interface ICustomerService
    {
      public  Task AddCustomer(Customer addCustomer);
        public  List<Customer> GetAllCustomers();
        public Task<Customer> GetCustomerById(int id);
        public Task EditCustomer(Customer editCustomer, string UniqueFilename);
        public Task<Customer> CustomerProfile(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCustomer(Customer addCustomer)
        {
            await _context.Customers.AddAsync(addCustomer);
            await _context.SaveChangesAsync();
            
        }

        public async Task EditCustomer(Customer editCustomer,string UniqueFilename)
        {
            var cust = await _context.Customers.FindAsync(editCustomer.CustomerId);
            //  cust.Name = editCustomer.Name;
            cust.FirstName = editCustomer.FirstName;
            cust.LastName = editCustomer.LastName;
            cust.DateOfBirth = editCustomer.DateOfBirth;
            cust.Gender = editCustomer.Gender;
            cust.City = editCustomer.City;
            cust.Address = editCustomer.Address;
            cust.Contact = editCustomer.Contact;
            if (UniqueFilename!=null) { cust.ProfilePic = UniqueFilename; }
           
            
            _context.Customers.Update(cust);
            await _context.SaveChangesAsync();
        }

        public List<Customer> GetAllCustomers()
        {
             var custList = _context.Customers.Include(x => x.AppUser).ToList();
            //var custList = _context.Customers.Include(x => x.AppUser).Select(x => new
            //{
            //    Name = x.Name,
            //    Gender = x.Gender,
            //    Contact = x.Contact,
            //    City = x.City,
            //    Address = x.Address,
            //   AppUser = x.AppUser.Select(y=> new AppUser {Email=y.Email })
            //    //AppUserId = x.AppUserId,


            //    //Email = x.AppUser.Email,
            //    //UserName = x.AppUser.UserName


            //});
            return custList;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
           var CustById= await _context.Customers.Include(x => x.AppUser).SingleOrDefaultAsync(x => x.AppUserId == id);
            return CustById;
        }
        //public async Task<Customer> CustomerProfile(int id)
        //{
        //    var customerProfile = await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == id);
        //    return customerProfile;
        //}

        public async Task<Customer> CustomerProfile(int id)
        {
            var customerProfile = await _context.Customers.SingleOrDefaultAsync(x => x.CustomerId == id);
            return customerProfile;
        }

    }
}
