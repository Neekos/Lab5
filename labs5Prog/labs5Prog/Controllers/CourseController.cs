using labs5Prog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace labs5Prog.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext db;
        public CourseController(ApplicationDbContext context)
        {
            context = db;
        }

        public async Task<IActionResult> Index()
        {

            return View( );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course getCourse = db.courses.Find(id);
            return View(getCourse);
        }
        public IActionResult Create()
        {
            ViewBag.Company = new SelectList(db.courses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course cours)
        {
            db.courses.Add(cours);
            await db.SaveChangesAsync();
            ViewBag.Company = new SelectList(db.currenses, "Id", "Name");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}