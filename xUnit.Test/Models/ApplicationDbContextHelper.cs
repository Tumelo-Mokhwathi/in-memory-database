using InMemoryDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnit.Test.Models
{
    public class ApplicationDbContextHelper
    {
        public async Task<ApplicationDbContext> ContextHelper()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Users").Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            context.Users.Add(new User
            {
                ID = Guid.NewGuid().ToString(),
                FirstName = "Mathew",
                Surname = "Lukas",
                Email = "mathew@gmail.com",
                ContactNo = "0712536473",
                Role = "Administrator"
            });

            context.Users.Add(new User
            {
                ID = Guid.NewGuid().ToString(),
                FirstName = "Catherine",
                Surname = "Smith",
                Email = "catherine@gmail.com",
                ContactNo = "0712536476",
                Role = "SuperAdministrator"
            });

            await context.SaveChangesAsync();

            return context;
        }
    }
}
