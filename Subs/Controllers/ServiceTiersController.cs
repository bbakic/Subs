using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Subs.Models;

namespace Subs.Controllers
{
    public class ServiceTiersController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceTiersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ServiceTiers
        public async Task<IActionResult> Index([FromQuery] int? service)
        {
            if (service == null)
            {
                return NotFound();
            }

            //var appDbContext = _context.ServiceTiers.Include(s => s.Service);
            var appDbContext = _context.ServiceTiers.Where(s => s.ServiceId == service).OrderBy(s => s.MinQuantity);
            ViewBag.service = await _context.Services.Include(s => s.ServiceCategory).FirstOrDefaultAsync(s => s.Id == service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ServiceTiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTier = await _context.ServiceTiers
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTier == null)
            {
                return NotFound();
            }

            return View(serviceTier);
        }

        // GET: ServiceTiers/Create
        public IActionResult Create([FromQuery] int? service)
        {
            if (service == null)
            {
                return NotFound();
            }

            ViewBag.service = _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefault(s => s.Id == service);
            return View();
        }

        // POST: ServiceTiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,MinQuantity,MaxQuantity,Price")] ServiceTier serviceTier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceTier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {service = serviceTier.ServiceId });
            }
            ViewBag.service = _context.Services
                            .Include(s => s.ServiceCategory)
                            .FirstOrDefault(s => s.Id == serviceTier.ServiceId);
            return View(serviceTier);
        }

        // GET: ServiceTiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTier = await _context.ServiceTiers.FindAsync(id);
            if (serviceTier == null)
            {
                return NotFound();
            }
            ViewBag.service = _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefault(s => s.Id == serviceTier.ServiceId);
            return View(serviceTier);
        }

        // POST: ServiceTiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceId,MinQuantity,MaxQuantity,Price")] ServiceTier serviceTier)
        {
            if (id != serviceTier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceTier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTierExists(serviceTier.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { service = serviceTier.ServiceId });
            }
            ViewBag.service = _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefault(s => s.Id == serviceTier.ServiceId);
            return View(serviceTier);
        }

        // GET: ServiceTiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceTier = await _context.ServiceTiers
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTier == null)
            {
                return NotFound();
            }
            ViewBag.service = _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefault(s => s.Id == serviceTier.ServiceId);
            return View(serviceTier);
        }

        // POST: ServiceTiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceTier = await _context.ServiceTiers.FindAsync(id);
            _context.ServiceTiers.Remove(serviceTier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { service = serviceTier.ServiceId });
        }

        private bool ServiceTierExists(int id)
        {
            return _context.ServiceTiers.Any(e => e.Id == id);
        }
    }
}
