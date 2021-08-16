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
    public class SubscriptionItemsController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriptionItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SubscriptionItems
        public async Task<IActionResult> Index([FromQuery] int? subscription)
        {
            if (subscription == null)
            {
                return NotFound();
            }

            ViewBag.subscription = await _context.Subscriptions.FindAsync(subscription);

            var appDbContext = _context.SubscriptionItems.Where(i => i.SubscriptionId == subscription).Include(s => s.Service).Include(s => s.Subscription);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SubscriptionItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionItem = await _context.SubscriptionItems
                .Include(s => s.Service)
                .Include(s => s.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscriptionItem == null)
            {
                return NotFound();
            }

            return View(subscriptionItem);
        }

        // GET: SubscriptionItems/Create
        public IActionResult Create([FromQuery] int? subscription)
        {
            if (subscription == null)
            {
                return NotFound();
            }
            
            var sub = _context.Subscriptions.Find(subscription);
            ViewBag.subscription = sub;
            ViewData["ServiceId"] = new SelectList(_context.Services.Where(s => s.ServiceCategoryId == sub.ServiceCategoryId), "Id", "Name");
            return View();
        }

        // POST: SubscriptionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubscriptionId,ServiceId,Quantity,Price,DiscountPercent")] SubscriptionItem subscriptionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscriptionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { subscription = subscriptionItem.SubscriptionId });
            }
            var sub = _context.Subscriptions.Find(subscriptionItem.SubscriptionId);
            ViewBag.subscription = sub;
            ViewData["ServiceId"] = new SelectList(_context.Services.Where(s => s.ServiceCategoryId == sub.ServiceCategoryId), "Id", "Name");
            return View(subscriptionItem);
        }

        // GET: SubscriptionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionItem = await _context.SubscriptionItems.FindAsync(id);
            if (subscriptionItem == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", subscriptionItem.ServiceId);
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", subscriptionItem.SubscriptionId);
            return View(subscriptionItem);
        }

        // POST: SubscriptionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubscriptionId,ServiceId,Quantity,Price,DiscountPercent")] SubscriptionItem subscriptionItem)
        {
            if (id != subscriptionItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscriptionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionItemExists(subscriptionItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { subscription = subscriptionItem.SubscriptionId });
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", subscriptionItem.ServiceId);
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", subscriptionItem.SubscriptionId);
            return View(subscriptionItem);
        }

        // GET: SubscriptionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionItem = await _context.SubscriptionItems
                .Include(s => s.Service)
                .Include(s => s.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscriptionItem == null)
            {
                return NotFound();
            }

            return View(subscriptionItem);
        }

        // POST: SubscriptionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscriptionItem = await _context.SubscriptionItems.FindAsync(id);
            _context.SubscriptionItems.Remove(subscriptionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { subscription = subscriptionItem.SubscriptionId });
        }

        private bool SubscriptionItemExists(int id)
        {
            return _context.SubscriptionItems.Any(e => e.Id == id);
        }
    }
}
