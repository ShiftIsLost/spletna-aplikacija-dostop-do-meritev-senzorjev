using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers
{

    public class ClientsController : Controller
    {
        private readonly AccessContext _context;

        public ClientsController(AccessContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Clients = from s in _context.Clients
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Clients = Clients.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Clients = Clients.OrderByDescending(s => s.LastName);
                    break;
                /*case "Date":
                    Clients = Clients.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    Clients = Clients.OrderByDescending(s => s.EnrollmentDate);
                    break;
                    */
                default:
                    Clients = Clients.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Client>.CreateAsync(Clients.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "Administrator, Manager, Staff")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

        var Client = await _context.Clients
        .Include(s => s.Accesses)
            .ThenInclude(e => e.Sensor)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ClientId == id);

            if (Client == null)
            {
                return NotFound();
            }

            return View(Client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName")] Client Client)
        {
            try
            {
                 if (ModelState.IsValid)
                {
                    _context.Add(Client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                 
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(Client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Client = await _context.Clients.FindAsync(id);
            if (Client == null)
            {
                return NotFound();
            }
            return View(Client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName")] Client Client)
        {
            if (id != Client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(Client.ClientId))
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
            return View(Client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (Client == null)
            {
                return NotFound();
            }

            return View(Client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(Client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
