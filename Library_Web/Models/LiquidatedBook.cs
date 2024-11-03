using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class LiquidatedBook
{
    public int LiquidatedId { get; set; }

    public int BookId { get; set; }

    public int CopyId { get; set; }

    public DateTime LiquidatedDate { get; set; }

    public decimal Price { get; set; }

    public int? LiquidatedBy { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual BookCopy Copy { get; set; } = null!;
}
