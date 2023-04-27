using labs5Prog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace labs5Prog.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Course>? getCourse = db.courses.Include(x => x.Currency);
            return View(await getCourse.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course getCourse = db.courses.Find(id);
            //ViewBag.getCurr = db.currenses.Where(p => p.Id == getCourse.CurrencyId.GetValueOrDefault());
            return View(getCourse);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Curr = new SelectList(db.currenses, "Id","Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course cours)
        {
            db.courses.Add(cours);
            await db.SaveChangesAsync();
            ViewBag.Curr = new SelectList(db.currenses, "Id", "Code");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Course? course = await db.courses.FirstOrDefaultAsync(p => p.Id == id);
                ViewBag.Curr = new SelectList(db.currenses, "Id", "Code");
                if (course != null) return View(course);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            db.courses.Update(course);
            ViewBag.Curr = new SelectList(db.currenses, "Id", "Code");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = db.courses.Find(id);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id != null)
            {
                Course course = new Course { Id = id.Value };
                db.Entry(course).State = EntityState.Deleted;
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