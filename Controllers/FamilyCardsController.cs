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
    public class FamilyCardsController : Controller
    {
        private readonly OrderSystemDatabaseContext _context;

        public FamilyCardsController(OrderSystemDatabaseContext context)
        {
            _context = context;
        }

        // GET: FamilyCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.FamilyCards.ToListAsync());
        }

        // GET: FamilyCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyCard = await _context.FamilyCards
                .FirstOrDefaultAsync(m => m.FamilyCardId == id);
            if (familyCard == null)
            {
                return NotFound();
            }

            return View(familyCard);
        }

        // GET: FamilyCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FamilyCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyCardId,FamilyCardBalance,FamilyCardBonusPercent")] FamilyCard familyCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(familyCard);
        }

        // GET: FamilyCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyCard = await _context.FamilyCards.FindAsync(id);
            if (familyCard == null)
            {
                return NotFound();
            }
            return View(familyCard);
        }

        // POST: FamilyCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FamilyCardId,FamilyCardBalance,FamilyCardBonusPercent")] FamilyCard familyCard)
        {
            if (id != familyCard.FamilyCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyCardExists(familyCard.FamilyCardId))
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
            return View(familyCard);
        }

        // GET: FamilyCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyCard = await _context.FamilyCards
                .FirstOrDefaultAsync(m => m.FamilyCardId == id);
            if (familyCard == null)
            {
                return NotFound();
            }

            return View(familyCard);
        }

        // POST: FamilyCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var familyCard = await _context.FamilyCards.FindAsync(id);
            _context.FamilyCards.Remove(familyCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyCardExists(int id)
        {
            return _context.FamilyCards.Any(e => e.FamilyCardId == id);
        }
    }
}
