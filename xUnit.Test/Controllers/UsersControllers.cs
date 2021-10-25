using InMemoryDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using xUnit.Test.Models;
using Xunit;

namespace xUnit.Test.Controllers
{
    public class UsersControllers
    {
        [Fact]
        public async Task ItGetDataSuccessfully()
        {
            var context = new ApplicationDbContextHelper().ContextHelper();
            var controller = new InMemoryDatabase.Controllers.UsersController(await context);
            var result = await controller.GetAsync().ConfigureAwait(false) as OkObjectResult;

            Assert.NotNull(result.Value);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ItGetByIdSuccessfully()
        {
            var Id = "c3c663a3-59cd-4b61-85c5-b35574aad074";

            var context = new ApplicationDbContextHelper().ContextHelper();
            var controller = new InMemoryDatabase.Controllers.UsersController(await context);
            var result = await controller.GetByIdAsync(Id).ConfigureAwait(false) as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ItCreateSuccessfully()
        {
            User user = new User
            {
                ID = Guid.NewGuid().ToString(),
                FirstName = "Pieter",
                Surname = "Cachet",
                Email = "pieter@gmail.com",
                ContactNo = "0712536473",
                Role = "Administrator"
            };

            var context = new ApplicationDbContextHelper().ContextHelper();
            var controller = new InMemoryDatabase.Controllers.UsersController(await context);
            var result = await controller.CeateAsync(user).ConfigureAwait(false) as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
