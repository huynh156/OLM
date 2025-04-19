using System.ComponentModel.DataAnnotations;

namespace OLM.ViewModels
{
    public class UserVM
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "Student";

        public DateTime? CreatedAt { get; set; }

        public string? Address { get; set; }

        public string? Username { get; set; }

        public string? PhoneNumber { get; set; }
        public int TotalEnrolledCourses { get; set; } // Số khóa học đã đăng ký

        public int CompletedCourses { get; set; } 

        public DateTime? ActivationDate { get; set; } 

        public string StudentStatus { get; set; } = "Active"; // Trạng thái học viên (Active, Completed, Suspended)

    }
}
