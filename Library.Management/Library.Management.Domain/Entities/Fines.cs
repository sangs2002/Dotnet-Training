namespace Library.Management.Domain.Entities
{
    public class Fines
    {

            public int FineId { get; private set; }
            public int BorrowingId { get; private set; }
            public int MemberId { get; private set; }
            public decimal Amount { get; private set; }
            public bool IsPaid { get; private set; }
            public DateTime CreatedAt { get; private set; }

            public Borrowing Borrowing { get; private set; } = null!;
            public Members Members { get; private set; } = null!;


        private Fines() { }

          
        public Fines(int fineId, int borrowingId, int memberId, decimal amount, bool isPaid, DateTime createdAt)
        {
            FineId = fineId;
            BorrowingId = borrowingId;
            MemberId = memberId;
            Amount = amount;
            IsPaid = isPaid;
            CreatedAt = createdAt;
        }

        public static Fines Create(int borrowingId,int memberId,decimal amount)
            {
                if (amount <= 0)
                    throw new Exception("Fine amount must be greater than zero");

                return new Fines
                {
                    BorrowingId = borrowingId,
                    MemberId = memberId,
                    Amount = amount,
                    IsPaid = false,
                    CreatedAt = DateTime.UtcNow
                };
            }

            public void MarkAsPaid()
            {
                IsPaid = true;
            }
        }

    }

