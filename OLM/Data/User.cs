using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    public int? PhoneNumber { get; set; }

    public string? RandomKey { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
