using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class MemberGroup
{
    public int GroupId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int Fee { get; set; }

    public virtual ICollection<BookAccessForMemberGroup> BookAccessForMemberGroups { get; set; } = new List<BookAccessForMemberGroup>();
}
