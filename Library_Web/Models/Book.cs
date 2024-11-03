using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int PublishYear { get; set; }

    public int MaxCopiesPerShelf { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public int PublisherId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public decimal Price { get; set; }

    public int AvailableCopies { get; set; }

    public decimal DamageFee { get; set; }

    public bool Warehouse { get; set; }

    public byte[] Cover { get; set; } = null!;

    public string PdfLink { get; set; } = null!;

    public int? Views { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual BookCopy? BookCopy { get; set; }

    public virtual ICollection<BookInGroup> BookInGroups { get; set; } = new List<BookInGroup>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<FavoritesList> FavoritesLists { get; set; } = new List<FavoritesList>();

    public virtual ICollection<LiquidatedBook> LiquidatedBooks { get; set; } = new List<LiquidatedBook>();

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Supplier Supplier { get; set; } = null!;
}
