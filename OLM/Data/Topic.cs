using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class Topic
{
    public int TopicId { get; set; }

    public int ChapterId { get; set; }

    public string TopicName { get; set; } = null!;

    public string? Description { get; set; }

    public string? YouTubeLink { get; set; }

    public string? DocumentLink { get; set; }

    public int OrderIndex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Chapter Chapter { get; set; } = null!;

    public virtual ICollection<ProgressTracking> ProgressTrackings { get; set; } = new List<ProgressTracking>();
}
