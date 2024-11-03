using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}
