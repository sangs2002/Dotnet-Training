namespace Library.Management.Application.Interface.Borrow
{
    public interface IBorrowService
    {
        Task<BorrowResponse> BorrowAsync(int memberId, int bookId, CancellationToken cancellationToken);
        Task<BorrowResponse> ReturnAsync(int borrowingId);
        Task<List<BorrowResponse>> GetAllBorrow(CancellationToken cancellationToken);
    }
}
