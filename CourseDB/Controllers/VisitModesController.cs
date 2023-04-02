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
    public class VisitModesController : Controller
    {
        private readonly SportComplexContext _context;

        public VisitModesController(SportComplexContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.VisitModes != null ? 
                          View(await _context.VisitModes.ToListAsync()) :
                          Problem("Entity set 'SportComplexContext.VisitModes'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VisitModes == null)
            {
                return NotFound();
            }

            var visitMode = await _context.VisitModes
                .FirstOrDefaultAsync(m => m.VisitModeId == id);
            if (visitMode == null)
            {
                return NotFound();
            }

            return View(visitMode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitModeId,Name")] VisitMode visitMode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitMode);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VisitModes == null)
            {
                return NotFound();
            }

            var visitMode = await _context.VisitModes.FindAsync(id);
            if (visitMode == null)
            {
                return NotFound();
            }
            return View(visitMode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitModeId,Name")] VisitMode visitMode)
        {
            if (id != visitMode.VisitModeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitModeExists(visitMode.VisitModeId))
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
            return View(visitMode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VisitModes == null)
            {
                return NotFound();
            }

            var visitMode = await _context.VisitModes
                .FirstOrDefaultAsync(m => m.VisitModeId == id);
            if (visitMode == null)
            {
                return NotFound();
            }

            return View(visitMode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VisitModes == null)
            {
                return Problem("Entity set 'SportComplexContext.VisitModes'  is null.");
            }
            var visitMode = await _context.VisitModes.FindAsync(id);
            if (visitMode != null)
            {
                _context.VisitModes.Remove(visitMode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitModeExists(int id)
        {
          return (_context.VisitModes?.Any(e => e.VisitModeId == id)).GetValueOrDefault();
        }
    }
}
