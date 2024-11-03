using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoanDTO
    {
        public int UserId { get; set; }

        public int CopyId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Fine { get; set; }

        public decimal BorrowFee { get; set; }

        public string Status { get; set; } = null!;
    }
}
