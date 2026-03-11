namespace Library.Management.Application.Interface.Common
{
    public interface ILibraryDbContext
    {
         DbSet<Books> Books { get; }

         DbSet<Borrowing> Borrowings { get; }

         DbSet<Fines> Fines { get;  }

         DbSet<Members> Members { get; }
    }
}
