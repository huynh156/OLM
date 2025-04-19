using System;
using System.Collections.Generic;

namespace OLM.Data;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public int CourseId { get; set; }

    public string ChapterName { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderIndex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? VideoLink { get; set; }

    public string? DocumentLink { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
