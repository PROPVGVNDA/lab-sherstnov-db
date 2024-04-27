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
    public class ClassRegistrationsController : Controller
    {
        private readonly PostgresContext _context;

        public ClassRegistrationsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: ClassRegistrations
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Classregistrations.Include(c => c.Class).Include(c => c.Member);
            return View(await postgresContext.ToListAsync());
        }

        // GET: ClassRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.Classregistrations
                .Include(c => c.Class)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classRegistration == null)
            {
                return NotFound();
            }

            return View(classRegistration);
        }

        // GET: ClassRegistrations/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            return View();
        }

        // POST: ClassRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,ClassId,RegistrationDate")] ClassRegistration classRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", classRegistration.ClassId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", classRegistration.MemberId);
            return View(classRegistration);
        }

        // GET: ClassRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.Classregistrations.FindAsync(id);
            if (classRegistration == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", classRegistration.ClassId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", classRegistration.MemberId);
            return View(classRegistration);
        }

        // POST: ClassRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,ClassId,RegistrationDate")] ClassRegistration classRegistration)
        {
            if (id != classRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassRegistrationExists(classRegistration.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", classRegistration.ClassId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", classRegistration.MemberId);
            return View(classRegistration);
        }

        // GET: ClassRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.Classregistrations
                .Include(c => c.Class)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classRegistration == null)
            {
                return NotFound();
            }

            return View(classRegistration);
        }

        // POST: ClassRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classRegistration = await _context.Classregistrations.FindAsync(id);
            if (classRegistration != null)
            {
                _context.Classregistrations.Remove(classRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassRegistrationExists(int id)
        {
            return _context.Classregistrations.Any(e => e.Id == id);
        }
    }
}
