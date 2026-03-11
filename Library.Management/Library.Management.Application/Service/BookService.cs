namespace Library.Management.Application.Service
{
    public class BookService : IBookService
    {
        private readonly ILibraryDbContext _libraryDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(ILibraryDbContext libraryDbContext, IUnitOfWork unitOfWork)
        {
            _libraryDbContext = libraryDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<BooksResponse> AddBookAsync(CreateBookRequest request)
        {

            var Books = new Books (
                request.Title,
                request.Author,
                request.ISBN,
                request.TotalCopies,
                request.Genre
                );

           await _libraryDbContext.Books.AddAsync(Books);

           await _unitOfWork.CommitAsync();
            

            return new BooksResponse
            {
                Title = Books.Title,
                Author = Books.Author,
                Genre = Books.Genre,
                ISBN = Books.ISBN,
                TotalCopies = Books.TotalCopies,
                AvailableCopies = Books.AvailableCopies,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public Task<List<Books>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            var getbooks = _libraryDbContext.Books.ToListAsync(cancellationToken);

            return getbooks;
        }

        public async Task<List<Books>> GetBooksAsync(string? author, string? genre)
        {
            var query = _libraryDbContext.Books.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));

            }
            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Contains(genre));

            }

            var result = await query.ToListAsync();
            
            return result;

        }

        public async Task UpdateBookAsync(UpdateBooksRequest books)
        {
            var existingbook = await _libraryDbContext.Books.FirstOrDefaultAsync(b=> b.BookId == books.BookId);

            if (existingbook == null)
            {
                throw new InvalidOperationException("Book Not Found");
            }

            existingbook.UpdateBook(

                existingbook.Title,
                existingbook.Author,
                existingbook.ISBN,
                existingbook.Genre,
                existingbook.IsActive,
                existingbook.TotalCopies,
                existingbook.AvailableCopies
                );

            await _unitOfWork.CommitAsync();

        }
    }

}
