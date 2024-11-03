using System;
using System.Collections.Generic;

namespace Models;

public partial class BookCopy
{
    public int BookId { get; set; }

    public string CopiesNumber { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public bool Warehouse { get; set; }

    public int ShelfId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<LiquidatedBook> LiquidatedBooks { get; set; } = new List<LiquidatedBook>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual Bookshelf Shelf { get; set; } = null!;
}
