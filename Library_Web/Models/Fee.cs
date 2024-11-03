using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Fee
{
    public int FeeId { get; set; }

    public string FeeType { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public string Name { get; set; } = null!;
}
