using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string OfficeLocation { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public int EstablishedYear { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Instructor>? Instructors { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
