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
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index([FromQuery] int? category)
        {
            if (category == null)
            {
                return NotFound();
            }

            if (TempData["modal"] != null)
            {
                ViewBag.modal = TempData["modal"];
                ViewBag.msg = "Service is being used and can not be deleted!";
            }
            var appDbContext = _context.Services.Where(s => s.ServiceCategoryId == category).OrderBy(s => s.Name);
            ViewBag.category = await _context.ServiceCategories.FindAsync(category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create([FromQuery] int? category)
        {
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.category = _context.ServiceCategories.Find(category);
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceCategoryId,Name")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { category = service.ServiceCategoryId });
            }
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Name", service.ServiceCategoryId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceCategoryId,Name")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { category = service.ServiceCategoryId });
            }
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Name", service.ServiceCategoryId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            if (await _context.ServiceTiers.FirstOrDefaultAsync(s => s.ServiceId == id) != null ||
                await _context.SubscriptionItems.FirstOrDefaultAsync(s => s.ServiceId == id) != null ||
                await _context.SubsVersionItems.FirstOrDefaultAsync(s => s.ServiceId == id) != null ||
                await _context.JournalItems.FirstOrDefaultAsync(s => s.ServiceId == id) != null ||
                await _context.ExpenseItems.FirstOrDefaultAsync(s => s.ServiceId == id) != null)
            {
                TempData["modal"] = service.Name;
                return RedirectToAction(nameof(Index), new { category = service.ServiceCategoryId } );
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { category = service.ServiceCategoryId });
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
