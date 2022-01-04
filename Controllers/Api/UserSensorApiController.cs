using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/usersensor")]
    [ApiController]
    public class UserSensorApiController : ControllerBase
    {
        private readonly AccessContext _context;

        public UserSensorApiController(AccessContext context)
        {
            _context = context;
        }

        // GET: api/UserSensorApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSensor>>> GetUserSensor()
        {
            return await _context.UserSensor.ToListAsync();
        }

        // GET: api/UserSensorApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSensor>> GetUserSensor(int id)
        {
            var userSensor = await _context.UserSensor.FindAsync(id);

            if (userSensor == null)
            {
                return NotFound();
            }

            return userSensor;
        }

        // PUT: api/UserSensorApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSensor(int id, UserSensor userSensor)
        {
            if (id != userSensor.UserSensorId)
            {
                return BadRequest();
            }

            _context.Entry(userSensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSensorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserSensorApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSensor>> PostUserSensor(UserSensor userSensor)
        {
            _context.UserSensor.Add(userSensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSensor", new { id = userSensor.UserSensorId }, userSensor);
        }

        // DELETE: api/UserSensorApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSensor(int id)
        {
            var userSensor = await _context.UserSensor.FindAsync(id);
            if (userSensor == null)
            {
                return NotFound();
            }

            _context.UserSensor.Remove(userSensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSensorExists(int id)
        {
            return _context.UserSensor.Any(e => e.UserSensorId == id);
        }
    }
}
