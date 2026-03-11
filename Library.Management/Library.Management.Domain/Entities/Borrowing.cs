namespace Library.Management.Domain.Entities
{
    public class Borrowing
    {

            public int BorrowingId { get; private set; }

            public int BookId { get; private set; }
            public int MemberId { get; private set; }

            public DateOnly BorrowedDate { get; private set; }
            public DateOnly DueDate { get; private set; }
            public DateOnly? ReturnedDate { get; private set; }

            public BorrowStatus Status { get; private set; }

            public virtual Books Book { get; private set; } = null!;
            public virtual Members Member { get; private set; } = null!;
            public virtual ICollection<Fines> Fines { get; private set; } = new List<Fines>();


        private Borrowing() { }

        public Borrowing(int bookId,int memberId,DateOnly borrowedDate,DateOnly dueDate)
        {
            BookId = bookId;
            MemberId = memberId;
            BorrowedDate = borrowedDate;
            DueDate = dueDate;
            Status = BorrowStatus.Borrowed;
        }

        public void MarkReturned(DateOnly returnedDate)
            {
                ReturnedDate = returnedDate;
                Status = returnedDate > DueDate
                    ? BorrowStatus.Overdue
                    : BorrowStatus.Returned;
            }
        }


    }

