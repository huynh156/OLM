using System.ComponentModel.DataAnnotations;

namespace OLM.ViewModels
{
    public class CourseVM
    {   [Key]
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Image { get; set; }
    }
}
