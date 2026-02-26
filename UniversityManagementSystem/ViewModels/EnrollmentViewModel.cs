using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.ViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string? Grade { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public IEnumerable<SelectListItem>? Students { get; set; }
        public IEnumerable<SelectListItem>? Courses { get; set; }
    }
}
