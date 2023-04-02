using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseDB.Models;

namespace CourseDB.Controllers
{
    public class ClubCardsController : Controller
    {
        private readonly SportComplexContext _context;

        public ClubCardsController(SportComplexContext context)
        {
            _context = context;
        }

   
        public async Task<IActionResult> Index()
        {
            var sportComplexContext = _context.ClubCards.Include(c => c.Client).Include(c => c.VisitMode);
            return View(await sportComplexContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubCards == null)
            {
                return NotFound();
            }

            var clubCard = await _context.ClubCards
                .Include(c => c.Client)
                .Include(c => c.VisitMode)
                .FirstOrDefaultAsync(m => m.ClubCardId == id);
            if (clubCard == null)
            {
                return NotFound();
            }

            return View(clubCard);
        }

       
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["VisitModeId"] = new SelectList(_context.VisitModes, "VisitModeId", "VisitModeId");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubCardId,ClientId,VisitModeId,Discount")] ClubCard clubCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", clubCard.ClientId);
            ViewData["VisitModeId"] = new SelectList(_context.VisitModes, "VisitModeId", "VisitModeId", clubCard.VisitModeId);
            return View(clubCard);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubCards == null)
            {
                return NotFound();
            }

            var clubCard = await _context.ClubCards.FindAsync(id);
            if (clubCard == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", clubCard.ClientId);
            ViewData["VisitModeId"] = new SelectList(_context.VisitModes, "VisitModeId", "VisitModeId", clubCard.VisitModeId);
            return View(clubCard);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClubCardId,ClientId,VisitModeId,Discount")] ClubCard clubCard)
        {
            if (id != clubCard.ClubCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubCardExists(clubCard.ClubCardId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", clubCard.ClientId);
            ViewData["VisitModeId"] = new SelectList(_context.VisitModes, "VisitModeId", "VisitModeId", clubCard.VisitModeId);
            return View(clubCard);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClubCards == null)
            {
                return NotFound();
            }

            var clubCard = await _context.ClubCards
                .Include(c => c.Client)
                .Include(c => c.VisitMode)
                .FirstOrDefaultAsync(m => m.ClubCardId == id);
            if (clubCard == null)
            {
                return NotFound();
            }

            return View(clubCard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubCards == null)
            {
                return Problem("Entity set 'SportComplexContext.ClubCards'  is null.");
            }
            var clubCard = await _context.ClubCards.FindAsync(id);
            if (clubCard != null)
            {
                _context.ClubCards.Remove(clubCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubCardExists(int id)
        {
          return (_context.ClubCards?.Any(e => e.ClubCardId == id)).GetValueOrDefault();
        }
    }
}
