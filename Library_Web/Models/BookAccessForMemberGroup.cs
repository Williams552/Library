using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class BookAccessForMemberGroup
{
    public int Id { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int BookGroupId { get; set; }

    public int GroupId { get; set; }

    public virtual BookGroup BookGroup { get; set; } = null!;

    public virtual MemberGroup Group { get; set; } = null!;
}
