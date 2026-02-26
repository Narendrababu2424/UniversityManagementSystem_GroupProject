using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseMarks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseMark.Include(c => c.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseMark = await _context.CourseMark
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseMark == null)
            {
                return NotFound();
            }

            return View(courseMark);
        }

        // GET: CourseMarks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email");
            return View();
        }

        // POST: CourseMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseNumber,Mark,StudentId")] CourseMark courseMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", courseMark.StudentId);
            return View(courseMark);
        }

        // GET: CourseMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseMark = await _context.CourseMark.FindAsync(id);
            if (courseMark == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", courseMark.StudentId);
            return View(courseMark);
        }

        // POST: CourseMarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseNumber,Mark,StudentId")] CourseMark courseMark)
        {
            if (id != courseMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseMarkExists(courseMark.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", courseMark.StudentId);
            return View(courseMark);
        }

        // GET: CourseMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseMark = await _context.CourseMark
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseMark == null)
            {
                return NotFound();
            }

            return View(courseMark);
        }

        // POST: CourseMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseMark = await _context.CourseMark.FindAsync(id);
            if (courseMark != null)
            {
                _context.CourseMark.Remove(courseMark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseMarkExists(int id)
        {
            return _context.CourseMark.Any(e => e.Id == id);
        }
    }
}
