using System;
using System.Collections.Generic;

namespace Library_Web.Models;

public partial class Loan
{
    public int LoanId { get; set; }

    public int UserId { get; set; }

    public int CopyId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public DateTime DueDate { get; set; }

    public decimal Fine { get; set; }

    public decimal BorrowFee { get; set; }

    public string Status { get; set; } = null!;

    public virtual BookCopy Copy { get; set; } = null!;

    public virtual ICollection<Penalty> Penalties { get; set; } = new List<Penalty>();

    public virtual Member User { get; set; } = null!;
}
