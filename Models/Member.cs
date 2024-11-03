using System;
using System.Collections.Generic;

namespace Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public string IdCardNumber { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public int? GroupId { get; set; }

    public int? MembershipFee { get; set; }

    public string? ResetPin { get; set; }

    public DateTime? ResetPinExpire { get; set; }

    public string? Role { get; set; }

    public DateTime? MembershipFeeDueDate { get; set; }

    public decimal? Balance { get; set; }

    public virtual ICollection<FavoritesList> FavoritesLists { get; set; } = new List<FavoritesList>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
