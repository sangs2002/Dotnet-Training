namespace Library.Management.Application.DTOs.Response
{
    public class FineResponse
    {
        public int FineId { get; set; }
        public int BorrowingId { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }

        public Borrowing Borrowing { get; set; } = null!;
        public Members Members { get; set; } = null!;

    }

}
