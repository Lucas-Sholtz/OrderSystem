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
    public class ProductOrderPairsController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public ProductOrderPairsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: ProductOrderPairs
        public async Task<IActionResult> Index()
        {
            var orderSystemDatabaseContext = _context.ProductOrderPairs.Include(p => p.Order).Include(p => p.Product);
            return View(await orderSystemDatabaseContext.ToListAsync());
        }
        public async Task<IActionResult> ByOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Orders", "Index");
            }

            ViewBag.OrderId = id;

            var pairsInOrder = _context.ProductOrderPairs.Where(p => p.OrderId == id).Include(p=>p.Order).Include(p=>p.Product);

            return View(await pairsInOrder.ToListAsync());
        }
        // GET: ProductOrderPairs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderPair = await _context.ProductOrderPairs
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.PairId == id);
            if (productOrderPair == null)
            {
                return NotFound();
            }

            return View(productOrderPair);
        }

        // GET: ProductOrderPairs/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: ProductOrderPairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PairId,ProductQuantity,ProductId,OrderId")] ProductOrderPair productOrderPair)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productOrderPair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", productOrderPair.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productOrderPair.ProductId);
            return View(productOrderPair);
        }

        // GET: ProductOrderPairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderPair = await _context.ProductOrderPairs.FindAsync(id);
            if (productOrderPair == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", productOrderPair.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productOrderPair.ProductId);
            return View(productOrderPair);
        }

        // POST: ProductOrderPairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PairId,ProductQuantity,ProductId,OrderId")] ProductOrderPair productOrderPair)
        {
            if (id != productOrderPair.PairId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOrderPair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOrderPairExists(productOrderPair.PairId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", productOrderPair.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productOrderPair.ProductId);
            return View(productOrderPair);
        }

        // GET: ProductOrderPairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderPair = await _context.ProductOrderPairs
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.PairId == id);
            if (productOrderPair == null)
            {
                return NotFound();
            }

            return View(productOrderPair);
        }

        // POST: ProductOrderPairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productOrderPair = await _context.ProductOrderPairs.FindAsync(id);
            _context.ProductOrderPairs.Remove(productOrderPair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOrderPairExists(int id)
        {
            return _context.ProductOrderPairs.Any(e => e.PairId == id);
        }
    }
}
