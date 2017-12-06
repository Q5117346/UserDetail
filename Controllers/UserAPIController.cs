using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UserDetail.Models;
using UserDetail.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserDetail.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserAPIController : Controller
    {
        private readonly UserContext _context;

        public UserAPIController(UserContext context)
        {
            _context = context;
        }

        // GET: api/user
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        //GET: api/user/1
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // PUT: api/user/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return NoContent();
            }
            else
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }

        // PUT: api/user/useredit/1
        [HttpPut("useredit/{id}")]
        public async Task<IActionResult> UserEdit([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return NoContent();
            }
            else
            {
                User use = _context.User.SingleOrDefault(s => s.id == id);
                user.canBuy = use.canBuy;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }

        // PUT: api/user/staffedit/1
        [HttpPut("staffedit/{id}")]
        public async Task<IActionResult> StaffEdit([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return NoContent();
            }
            else
            {
                User use = _context.User.SingleOrDefault(s => s.id == id);
                user.name = use.name;
                user.email = use.email;
                user.phoneNumber = use.phoneNumber;
                user.address = use.address;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }

        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}