using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class ProgressTracking
{
    public int ProgressId { get; set; }

    public int EnrollmentId { get; set; }

    public int TopicId { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;

    public virtual Topic Topic { get; set; } = null!;
}
