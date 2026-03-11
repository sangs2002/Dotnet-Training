namespace Library.Management.Application.Interface.Book
{
    public interface IBookService
    {
        Task<BooksResponse> AddBookAsync(CreateBookRequest request);

        Task<List<Books>> GetAllBooksAsync(CancellationToken cancellationToken);

        Task<List<Books>> GetBooksAsync(string? author, string? genre);

        Task UpdateBookAsync(UpdateBooksRequest books);
    }
}
