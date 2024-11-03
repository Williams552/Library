﻿using System;
using System.Collections.Generic;

namespace Models;

public partial class BookGroup
{
    public int GroupId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<BookAccessForMemberGroup> BookAccessForMemberGroups { get; set; } = new List<BookAccessForMemberGroup>();

    public virtual ICollection<BookInGroup> BookInGroups { get; set; } = new List<BookInGroup>();
}
