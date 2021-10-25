using InMemoryDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAsync() => new OkObjectResult(
            new {
                code = System.Net.HttpStatusCode.OK,
                result = await _context.Users.ToListAsync(),
                source = "get-users"
            });

        [HttpGet]
        [Route("search-user")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id).ConfigureAwait(false);

            try
            {
                return new OkObjectResult(new
                {
                    code = System.Net.HttpStatusCode.OK,
                    user,
                    source = "get-user-by-id"
                });
            }
            catch
            {
                return new ForbidResult("Failed to delete user!");
            }
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CeateAsync(User user)
        {
            var model = new User
            {
                ID = Guid.NewGuid().ToString(),
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                ContactNo = user.ContactNo,
                Role = user.Role
            };

            _context.Add(model);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new OkObjectResult(new
            {
                code = System.Net.HttpStatusCode.OK,
                result = new
                {
                    message = "User Created Succesfully"
                },
                source = "create-user"
            });
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var model = await _context.Users.FirstOrDefaultAsync(u => u.ID == user.ID).ConfigureAwait(false);

            model.FirstName = user.FirstName;
            model.Surname = user.Surname;
            model.Email = user.Email;
            model.ContactNo = user.ContactNo;
            model.Role = user.Role;

            if (model == null)
            {
                return NotFound("User not found!");
            }

            _context.Update(model);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new OkObjectResult(new
            {
                code = System.Net.HttpStatusCode.OK,
                result = new
                {
                    message = "User Updated Succesfully"
                },
                source = "update-user"
            });
        }

        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            _context.Remove(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new OkObjectResult(new
            {
                code = System.Net.HttpStatusCode.OK,
                result = new
                {
                    message = "User Deleted Succesfully"
                },
                source = "delete-user"
            });
        }
    }
}
