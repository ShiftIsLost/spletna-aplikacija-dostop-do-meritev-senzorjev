using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using web.Data;
using web.Models;

namespace web.Controllers
{
    [Authorize(Roles ="Administrator, Manager, Staff, User")]
    public class SensorController : Controller
    {
        private readonly AccessContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SensorController(AccessContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Sensor
        public async Task<IActionResult> Index()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            if (_context.UserRoles.Where(r => r.UserId == usr.Id).Any(r => r.RoleId == "1" | r.RoleId == "2")){
                return View(await _context.Sensor.ToListAsync());
            }
            var query = from Sensor in _context.Sensor
                join UserSensor in _context.UserSensor on Sensor.SensorId equals UserSensor.SensorId
                where UserSensor.ApplicationUserId == usr.Id
                select Sensor;
            return View(query.ToList());
        }

        // GET: Sensor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.location = new Location{LocationId = 0, Name = "", Address = "", x = 0, y = 0};
            var sensor = await _context.Sensor.FirstOrDefaultAsync(m => m.SensorId == id);
            if (sensor == null)
            {
                return NotFound();
            }
            if (sensor.LocationId != null)
            {
                ViewBag.location = await _context.Location.FirstOrDefaultAsync(m => m.LocationId == sensor.LocationId);
            }
            
            return View(sensor);
        }

        [Authorize(Roles ="Administrator, Manager")]
        // GET: Sensor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator, Manager")]
        public async Task<IActionResult> Create([Bind("SensorId,SensorName,Type,SerialNumber,Location,FirmwareVersion")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        // GET: Sensor/Edit/5
        [Authorize(Roles ="Administrator, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return View(sensor);
        }

        // POST: Sensor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator, Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("SensorId,SensorName,Type,SerialNumber,Location,FirmwareVersion")] Sensor sensor)
        {
            if (id != sensor.SensorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.SensorId))
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
            return View(sensor);
        }

        // GET: Sensor/Delete/5
        [Authorize(Roles ="Administrator, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensor
                .FirstOrDefaultAsync(m => m.SensorId == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // POST: Sensor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator, Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            _context.Sensor.Remove(sensor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.SensorId == id);
        }
    }
}
