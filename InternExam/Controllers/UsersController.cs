using InternExam.Data;
using InternExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> Add(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Users>>> Update(Users request)
        {
            var result = await _context.Users.FindAsync(request.UserId);
            if (result == null)
                return BadRequest("User not found.");

            result.UserFullName = request.UserFullName;
            result.UserBirthday = request.UserBirthday;
            result.UserGender = request.UserGender;
            result.UserCreatedAt = request.UserCreatedAt;
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<Users>>> Delete(int id)
        {
            var result = await _context.Users.FindAsync(id);
            if (result == null)
                return BadRequest("User not found.");

            _context.Users.Remove(result);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

    }
}
