using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersSystem;

namespace OrdersSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public OrdersController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orderSystemDatabaseContext = _context.Orders.Include(o => o.Address).Include(o => o.Client).Include(o => o.Courier).Include(o=>o.Address.Street).Include(o=>o.Address.Street.Town);
            return View(await orderSystemDatabaseContext.ToListAsync());
        }

        public async Task<IActionResult> ByCourier(int? id, string? name)
        {
            if (id == null)
                return RedirectToAction("Couriers", "Index");

            ViewBag.CourierId = id;
            ViewBag.CourierFullName = name;

            var ordersOfCourier = _context.Orders.Where(b => b.CourierId == id).Include(b => b.Courier).Include(b => b.Client).Include(b => b.Address);

            return View(await ordersOfCourier.ToListAsync());
        }

        public async Task<IActionResult> ByClient(int? id, string? name)
        {
            if (id == null)
                return RedirectToAction("Clients", "Index");

            ViewBag.ClientId = id;
            ViewBag.ClientFullName = name;

            var ordersOfClient = _context.Orders.Where(b => b.ClientId == id).Include(b => b.Courier).Include(b => b.Client).Include(b => b.Address);

            return View(await ordersOfClient.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.Client)
                .Include(o => o.Courier)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return RedirectToAction("ByOrder", "ProductOrderPairs", new { id = order.OrderId });
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Adresses, "AddressId", "AddressId");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientEmail");
            ViewData["CourierId"] = new SelectList(_context.Couriers, "CourierId", "CourierName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderStatus,ClientId,AddressId,CourierId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Adresses, "AddressId", "AddressId", order.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientEmail", order.ClientId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "CourierId", "CourierName", order.CourierId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Adresses, "AddressId", "AddressId", order.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientEmail", order.ClientId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "CourierId", "CourierName", order.CourierId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderStatus,ClientId,AddressId,CourierId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["AddressId"] = new SelectList(_context.Adresses, "AddressId", "AddressId", order.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientEmail", order.ClientId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "CourierId", "CourierName", order.CourierId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.Client)
                .Include(o => o.Courier)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
