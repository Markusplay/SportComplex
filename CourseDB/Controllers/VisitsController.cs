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
    public class VisitsController : Controller
    {
        private readonly SportComplexContext _context;

        public VisitsController(SportComplexContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sportComplexContext = _context.Visits.Include(v => v.Client).Include(v => v.Service);
            return View(await sportComplexContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Client)
                .Include(v => v.Service)
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitId,ServiceId,ClientId,Date")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", visit.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", visit.ServiceId);
            return View(visit);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", visit.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", visit.ServiceId);
            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitId,ServiceId,ClientId,Date")] Visit visit)
        {
            if (id != visit.VisitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.VisitId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", visit.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", visit.ServiceId);
            return View(visit);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Client)
                .Include(v => v.Service)
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visits == null)
            {
                return Problem("Entity set 'SportComplexContext.Visits'  is null.");
            }
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
          return (_context.Visits?.Any(e => e.VisitId == id)).GetValueOrDefault();
        }
    }
}
