using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemSellTask.DbCon;
using ItemSellTask.Models;

namespace ItemSellTask.Controllers
{
    public class ItemsController : Controller
    {
        private readonly DbConContext _context;

        public ItemsController(DbConContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbConContext = _context.Items.Include(i => i.Category);
            return View(await dbConContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Item item)
        {
            if (0 > item.ItemPrice )
            {
                ModelState.AddModelError("ItemPrice", "Item Price cannot be negetive.");
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", item.CategoryId);
                return View(item);
            }

            _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", item.CategoryId);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", item.CategoryId);
            return View(item);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'DbConContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Sell(long id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null && item.ItemCount > 0)
            {
                item.QuantitySold++;
                item.ItemCount--;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ItemExists(long id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
