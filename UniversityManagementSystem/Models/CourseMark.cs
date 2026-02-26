using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class CourseMark
    {
        public int Id { get; set; }

        [Required]
        public int CourseNumber { get; set; }

        [Required]
        public int Mark { get; set; }

        // Foreign Key
        public int StudentId { get; set; }

        // Navigation Property
        public Student? Student { get; set; }
    }
}