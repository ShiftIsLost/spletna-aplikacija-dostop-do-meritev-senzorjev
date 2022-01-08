using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using web.Data;
using web.Models;

namespace web.Controllers
{
    [Authorize(Roles ="Administrator, Manager")]
    public class UserSensorController : Controller
    {
        private readonly AccessContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSensorController(AccessContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UserSensor
        public async Task<IActionResult> Index()
        { 
            var accessContext = _context.UserSensor.Include(u => u.ApplicationUser).Include(u => u.Sensor);
            return View(await accessContext.ToListAsync());
        }

        // GET: UserSensor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSensor = await _context.UserSensor
                .Include(u => u.ApplicationUser)
                .Include(u => u.Sensor)
                .FirstOrDefaultAsync(m => m.UserSensorId == id);
            if (userSensor == null)
            {
                return NotFound();
            }

            return View(userSensor);
        }

        // GET: UserSensor/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["SensorId"] = new SelectList(_context.Sensor, "SensorId", "SensorName");
            return View();
        }

        // POST: UserSensor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserSensorId,SensorId,ApplicationUserId")] UserSensor userSensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", userSensor.ApplicationUserId);
            ViewData["SensorId"] = new SelectList(_context.Sensor, "SensorId", "SensorName", userSensor.SensorId);
            return View(userSensor);
        }

        // GET: UserSensor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSensor = await _context.UserSensor.FindAsync(id);
            if (userSensor == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userSensor.ApplicationUserId);
            ViewData["SensorId"] = new SelectList(_context.Sensor, "SensorId", "SensorId", userSensor.SensorId);
            return View(userSensor);
        }

        // POST: UserSensor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserSensorId,SensorId,ApplicationUserId")] UserSensor userSensor)
        {
            if (id != userSensor.UserSensorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSensorExists(userSensor.UserSensorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userSensor.ApplicationUserId);
            ViewData["SensorId"] = new SelectList(_context.Sensor, "SensorId", "SensorId", userSensor.SensorId);
            return View(userSensor);
        }

        // GET: UserSensor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSensor = await _context.UserSensor
                .Include(u => u.ApplicationUser)
                .Include(u => u.Sensor)
                .FirstOrDefaultAsync(m => m.UserSensorId == id);
            if (userSensor == null)
            {
                return NotFound();
            }

            return View(userSensor);
        }

        // POST: UserSensor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSensor = await _context.UserSensor.FindAsync(id);
            _context.UserSensor.Remove(userSensor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSensorExists(int id)
        {
            return _context.UserSensor.Any(e => e.UserSensorId == id);
        }
    }
}
