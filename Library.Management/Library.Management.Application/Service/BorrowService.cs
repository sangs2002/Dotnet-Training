namespace Library.Management.Application.Service
{
    public class BorrowService : IBorrowService
    {

        private readonly ILibraryDbContext _libraryDbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemberFactory _memberFactory;


        public BorrowService(ILibraryDbContext libraryDbContext, IUnitOfWork unitOfWork, IMemberFactory memberFactory)

        {
            _libraryDbContext = libraryDbContext;
            _unitOfWork = unitOfWork;
            _memberFactory = memberFactory;
        }
        public async Task<BorrowResponse> BorrowAsync(int memberId, int bookId, CancellationToken cancellationToken)
        {
            var member = await _libraryDbContext.Members.FindAsync(memberId,cancellationToken);

            if (member == null)
            {
                throw new Exception("Member Not Found");
            }

            var memberfactory = _memberFactory.Create(member.MembershipType, member.MemberId);

            var activeborrowing = await _libraryDbContext.Borrowings.CountAsync(b => b.MemberId == memberId && b.ReturnedDate == null);


            if (activeborrowing >= memberfactory.MaxBooksAllowed)
            {
                throw new Exception("Borrowing limit exceeded");
            }


            var hasUnpaidFines = await _libraryDbContext.Fines.AnyAsync(f => f.MemberId == memberId && !f.IsPaid);

            if (hasUnpaidFines)
                throw new Exception("Clear unpaid fines before borrowing");

            var book = await _libraryDbContext.Books.FindAsync(bookId);
                

             if(book == null)
            {
                throw new Exception("Book not Found");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new Exception("No copies available");
            }

            var borrowing = new Borrowing(
           bookId,
           memberId,
           DateOnly.FromDateTime(DateTime.UtcNow),
           DateOnly.FromDateTime(DateTime.UtcNow.AddDays(14))
       );

            book.DecreaseCopies();

            _libraryDbContext.Borrowings.Add(borrowing);
            await _unitOfWork.CommitAsync();

            var response = new BorrowResponse
            {
                BorrowingId = borrowing.BorrowingId,
                BookId = borrowing.BookId,
                MemberId = borrowing.MemberId,
                BorrowedDate = borrowing.BorrowedDate,
                ReturnedDate = borrowing.ReturnedDate,
                DueDate = borrowing.DueDate,
                Status = borrowing.Status,

            };

            return response;

        }

        public async Task<BorrowResponse> ReturnAsync(int borrowingId)
        {
            var borrowing = await _libraryDbContext.Borrowings.FindAsync(borrowingId);

            if (borrowing == null) {
                throw new Exception("Book not Found");
            }

            borrowing.MarkReturned(DateOnly.FromDateTime(DateTime.UtcNow));

            var lateDays = (borrowing.ReturnedDate.Value.DayNumber
                           - borrowing.DueDate.DayNumber);

            if (lateDays > 0)
            {
                var member = await _libraryDbContext.Members.FindAsync(borrowing.MemberId)!;
                var calculator = FineCalculatorStrategy.Resolve(member.MembershipType);

                var fine = calculator.Calculate(lateDays);

                _libraryDbContext.Fines.Add(Fines.Create(borrowing.BorrowingId, member.MemberId, fine));
            }

            await _unitOfWork.CommitAsync();

            var response = new BorrowResponse
            {
                BorrowingId = borrowing.BorrowingId,
                BookId = borrowing.BookId,
                MemberId = borrowing.MemberId,
                BorrowedDate = borrowing.BorrowedDate,
                ReturnedDate = borrowing.ReturnedDate,
                DueDate = borrowing.DueDate,
                Status = borrowing.Status,

            };

            return response;

        }

        public async Task<List<BorrowResponse>> GetAllBorrow(CancellationToken cancellationToken)
        {
            var borrows = await _libraryDbContext.Borrowings
                .Select(b => new BorrowResponse
                {
                    BorrowingId = b.BorrowingId,
                    BookId = b.BookId,
                    MemberId = b.MemberId,
                    BorrowedDate = b.BorrowedDate,
                    ReturnedDate = b.ReturnedDate,
                    DueDate = b.DueDate,
                    Status = b.Status,
                    
                })
                .ToListAsync(cancellationToken);

            return borrows;

        }

    }
}
