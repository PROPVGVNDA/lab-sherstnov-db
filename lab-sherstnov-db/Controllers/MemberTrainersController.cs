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
    public class MemberTrainersController : Controller
    {
        private readonly PostgresContext _context;

        public MemberTrainersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: MemberTrainers
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Membertrainers.Include(m => m.Member).Include(m => m.Trainer);
            return View(await postgresContext.ToListAsync());
        }

        // GET: MemberTrainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTrainer = await _context.Membertrainers
                .Include(m => m.Member)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTrainer == null)
            {
                return NotFound();
            }

            return View(memberTrainer);
        }

        // GET: MemberTrainers/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id");
            return View();
        }

        // POST: MemberTrainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,TrainerId,StartDate,EndDate")] MemberTrainer memberTrainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberTrainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberTrainer.MemberId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", memberTrainer.TrainerId);
            return View(memberTrainer);
        }

        // GET: MemberTrainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTrainer = await _context.Membertrainers.FindAsync(id);
            if (memberTrainer == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberTrainer.MemberId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", memberTrainer.TrainerId);
            return View(memberTrainer);
        }

        // POST: MemberTrainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,TrainerId,StartDate,EndDate")] MemberTrainer memberTrainer)
        {
            if (id != memberTrainer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberTrainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberTrainerExists(memberTrainer.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberTrainer.MemberId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "Id", memberTrainer.TrainerId);
            return View(memberTrainer);
        }

        // GET: MemberTrainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTrainer = await _context.Membertrainers
                .Include(m => m.Member)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTrainer == null)
            {
                return NotFound();
            }

            return View(memberTrainer);
        }

        // POST: MemberTrainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberTrainer = await _context.Membertrainers.FindAsync(id);
            if (memberTrainer != null)
            {
                _context.Membertrainers.Remove(memberTrainer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberTrainerExists(int id)
        {
            return _context.Membertrainers.Any(e => e.Id == id);
        }
    }
}
