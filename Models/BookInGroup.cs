using System;
using System.Collections.Generic;

namespace Models;

public partial class BookInGroup
{
    public int Id { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int BookId { get; set; }

    public int GroupId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual BookGroup Group { get; set; } = null!;
}
