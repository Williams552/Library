using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Penalty
{
    public int PenaltyId { get; set; }

    public int LoanId { get; set; }

    public decimal TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public string Status { get; set; } = null!;

    public int CoverTear { get; set; }

    public int SpineDamage { get; set; }

    public int PageLoss { get; set; }

    public int Writing { get; set; }

    public int OverDue { get; set; }

    public virtual Loan Loan { get; set; } = null!;
}
