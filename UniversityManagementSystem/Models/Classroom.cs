using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string? BuildingName { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}
