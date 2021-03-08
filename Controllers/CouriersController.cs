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
    public class CouriersController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public CouriersController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: Couriers
        public async Task<IActionResult> Index()
        {
            var orderSystemDatabaseContext = _context.Couriers.Include(c => c.Shop);
            return View(await orderSystemDatabaseContext.ToListAsync());
        }

        // GET: Couriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .Include(c => c.Shop)
                .FirstOrDefaultAsync(m => m.CourierId == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // GET: Couriers/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName");
            return View();
        }

        // POST: Couriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourierId,CourierName,CourierSurname,ShopId")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", courier.ShopId);
            return View(courier);
        }

        // GET: Couriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", courier.ShopId);
            return View(courier);
        }

        // POST: Couriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourierId,CourierName,CourierSurname,ShopId")] Courier courier)
        {
            if (id != courier.CourierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourierExists(courier.CourierId))
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
            ViewData["ShopId"] = new SelectList(_context.Shops, "ShopId", "ShopName", courier.ShopId);
            return View(courier);
        }

        // GET: Couriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .Include(c => c.Shop)
                .FirstOrDefaultAsync(m => m.CourierId == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: Couriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            _context.Couriers.Remove(courier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourierExists(int id)
        {
            return _context.Couriers.Any(e => e.CourierId == id);
        }
    }
}
