using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersSystem;

namespace OrdersSystem.Controllers
{
    public class StreetsController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public StreetsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: Streets
        public async Task<IActionResult> Index()
        {
            var orderSystemDatabaseContext = _context.Streets.Include(s => s.Town);
            return View(await orderSystemDatabaseContext.ToListAsync());
        }
        public async Task<IActionResult> ByTown(int? id, string? name, int? postal)
        {
            if (id == null)
                return RedirectToAction("Towns", "Index");
            //find streets from town
            ViewBag.TownId = id;
            ViewBag.TownName = name;
            ViewBag.TownPostCode = postal;

            var streetsFromTown = _context.Streets.Where(b => b.TownId == id).Include(b => b.Town);

            return View(await streetsFromTown.ToListAsync());
            //var orderSystemDatabaseContext = _context.Streets.Include(s => s.Town);
            //return View(await orderSystemDatabaseContext.ToListAsync());
        }

        // GET: Streets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.Town)
                .FirstOrDefaultAsync(m => m.StreetId == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // GET: Streets/Create
        public IActionResult Create()
        {
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownName");
            return View();
        }

        // POST: Streets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetId,StreetName,TownId")] Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Add(street);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownName", street.TownId);
            return View(street);
        }

        // GET: Streets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownName", street.TownId);
            return View(street);
        }

        // POST: Streets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreetId,StreetName,TownId")] Street street)
        {
            if (id != street.StreetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetExists(street.StreetId))
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
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownName", street.TownId);
            return View(street);
        }

        // GET: Streets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.Town)
                .FirstOrDefaultAsync(m => m.StreetId == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetExists(int id)
        {
            return _context.Streets.Any(e => e.StreetId == id);
        }
    }
}
