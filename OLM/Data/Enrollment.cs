using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime? EnrolledAt { get; set; }

    public double? Progress { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<ProgressTracking> ProgressTrackings { get; set; } = new List<ProgressTracking>();

    public virtual User User { get; set; } = null!;
}
