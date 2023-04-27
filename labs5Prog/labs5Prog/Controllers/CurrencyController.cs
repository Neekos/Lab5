using labs5Prog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace labs5Prog.Controllers
{
    public class CurrencyController : Controller
    {
        ApplicationDbContext db;
        public CurrencyController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Currency>? getCurrency = db.currenses;

            return View( await getCurrency.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Company = new SelectList(db.currenses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Currency currency)
        {
            db.currenses.Add(currency);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Currency? curr = await db.currenses.FirstOrDefaultAsync(p => p.Id == id);
                if (curr != null) return View(curr);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Currency curr)
        {
            db.currenses.Update(curr);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Currency curr = new Currency { Id = id.Value };
                db.Entry(curr).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}