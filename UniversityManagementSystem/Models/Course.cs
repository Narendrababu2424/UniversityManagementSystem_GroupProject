using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Range(1, 10)]
        public int Credits { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
