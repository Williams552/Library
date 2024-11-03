using System;
using System.Collections.Generic;

namespace Models;

public partial class Bookshelf
{
    public int ShelfId { get; set; }

    public int ColumnNumber { get; set; }

    public int RowNumber { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int? ShelfNumber { get; set; }

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
}
