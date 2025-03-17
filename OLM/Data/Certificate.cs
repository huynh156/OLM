using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int EnrollmentId { get; set; }

    public string CertificateUrl { get; set; } = null!;

    public DateTime? IssuedAt { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
