using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.ViewModels;

namespace UniversityManagementSystem.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course);

            return View(await enrollments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (enrollment == null)
                return NotFound();

            return View(enrollment);
        }

        public IActionResult Create()
        {
            var viewModel = GetEnrollmentViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = new Enrollment
                {
                    StudentId = viewModel.StudentId,
                    CourseId = viewModel.CourseId,
                    Grade = viewModel.Grade,
                    EnrollmentDate = viewModel.EnrollmentDate
                };

                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel = GetEnrollmentViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                return NotFound();

            var viewModel = GetEnrollmentViewModel();
            viewModel.Id = enrollment.Id;
            viewModel.StudentId = enrollment.StudentId;
            viewModel.CourseId = enrollment.CourseId;
            viewModel.Grade = enrollment.Grade;
            viewModel.EnrollmentDate = enrollment.EnrollmentDate;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnrollmentViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);

                enrollment.StudentId = viewModel.StudentId;
                enrollment.CourseId = viewModel.CourseId;
                enrollment.Grade = viewModel.Grade;
                enrollment.EnrollmentDate = viewModel.EnrollmentDate;

                _context.Update(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            viewModel = GetEnrollmentViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (enrollment == null)
                return NotFound();

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private EnrollmentViewModel GetEnrollmentViewModel()
        {
            return new EnrollmentViewModel
            {
                Students = _context.Students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.FirstName + " " + s.LastName
                }).ToList(),

                Courses = _context.Courses.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList(),

                EnrollmentDate = DateTime.Now
            };
        }
    }
}
