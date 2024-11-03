using System;
using System.Collections.Generic;

namespace Models;

public partial class ReadingProgress
{
    public int ProgressId { get; set; }

    public int MemberId { get; set; }

    public int BookId { get; set; }

    public int? ReadingProgress1 { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
