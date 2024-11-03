﻿using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Biography { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public string Avartar { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
