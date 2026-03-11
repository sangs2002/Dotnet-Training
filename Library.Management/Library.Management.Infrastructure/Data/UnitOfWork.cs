namespace Library.Management.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _libraryDbContext;

        public UnitOfWork(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task CommitAsync()
        {
           await _libraryDbContext.SaveChangesAsync();
        }
    }
}
