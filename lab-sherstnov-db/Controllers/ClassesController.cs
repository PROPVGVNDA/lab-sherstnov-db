using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_sherstnov_db.Models;

namespace lab_sherstnov_db.Controllers
{
    public class ClassesController : Controller
    {
        private readonly PostgresContext _context;

        public ClassesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Classes.Include(c => c.Trainer);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // lazy loading
            //var cclass = await _context.Classes
            //    .FirstOrDefaultAsync(m => m.Id == id);

            // eager loading
            //var cclass = await _context.Classes
            //        .Include(c => c.Trainer)
            //        .Include(c => c.ClassRegistrations)
            //        .ThenInclude(ccr => ccr.Member)
            //        .FirstOrDefaultAsync(c => c.Id == id);

            //explicit loading
            var cclass = await _context.Classes
                        .FirstOrDefaultAsync();
            _context.Entry(cclass).Reference(c => c.Trainer).Load();
            _context.Entry(cclass).Collection(c => c.ClassRegistrations).Load();
            foreach (var classRegistration in cclass.ClassRegistrations)
            {
                _context.Entry(classRegistration).Reference(ccr => ccr.Member).Load();
            }

            if (cclass == null)
            {
                return NotFound();
            }

            return View(cclass);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TrainerId,ClassTime,Duration,MaximumParticipants")] Class cclass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cclass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", cclass.TrainerId);
            return View(cclass);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cclass = await _context.Classes.FindAsync(id);
            if (cclass == null)
            {
                return NotFound();
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", cclass.TrainerId);
            return View(cclass);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TrainerId,ClassTime,Duration,MaximumParticipants")] Class cclass)
        {
            if (id != cclass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cclass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(cclass.Id))
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
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", cclass.TrainerId);
            return View(cclass);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cclass = await _context.Classes
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cclass == null)
            {
                return NotFound();
            }

            return View(cclass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cclass = await _context.Classes.FindAsync(id);
            if (cclass != null)
            {
                _context.Classes.Remove(cclass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DeleteResult));
        }

        public async Task<IActionResult> GetAverageDuration(int threshold)
        {
            var durations = _context.Classes
                .Where(c => c.MaximumParticipants > threshold)
                .OrderByDescending(c => c.Duration)
                .Select(c => c.Duration);

            if (await durations.AnyAsync())
            {
                var averageDuration = await durations.AverageAsync();
                return Json(new { AverageDuration = averageDuration });
            }
            else
            {
                return Json(new { AverageDuration = 0, Message = "No classes that exceed the specified participant threshold." });
            }
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteResult()
        {
            var relatedData = await _context.Classes
                .Include(m => m.ClassRegistrations)
                .ToListAsync();

            return View(relatedData);
        }
    }
}
