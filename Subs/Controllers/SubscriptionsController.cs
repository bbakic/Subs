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
    public class SubscriptionsController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index([FromQuery] int? customer)
        {
            if (customer != null)
            {
                ViewBag.customer = await _context.Customers.FindAsync(customer);
                var appDbContext = _context.Subscriptions.Where(s => s.CustomerId == customer).Include(s => s.Customer).Include(s => s.ServiceCategory).OrderBy(s => s.CreateDate + s.ServiceCategory.Name);
                return View(await appDbContext.ToListAsync());
            }
            else
            {
                var appDbContext = _context.Subscriptions.Include(s => s.Customer).Include(s => s.ServiceCategory).OrderBy(s => s.CreateDate + s.ServiceCategory.Name);
                return View(await appDbContext.ToListAsync());
            }
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Customer)
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.Name), "Id", "Name");
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories.OrderBy(c => c.Name), "Id", "Name");
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateDate,CustomerId,ServiceCategoryId,StartDate,ExpiryDate,RenewalDayOfMonth,IsAutomaticRenewal,FlatAmount")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", subscription.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Name", subscription.ServiceCategoryId);
            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.Name), "Id", "Name", subscription.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories.OrderBy(c => c.Name), "Id", "Name", subscription.ServiceCategoryId);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreateDate,CustomerId,ServiceCategoryId,StartDate,ExpiryDate,RenewalDayOfMonth,IsAutomaticRenewal,FlatAmount")] Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", subscription.CustomerId);
            ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Name", subscription.ServiceCategoryId);
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Customer)
                .Include(s => s.ServiceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            _context.SubscriptionItems.RemoveRange(_context.SubscriptionItems.Where(i => i.SubscriptionId == id));
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
