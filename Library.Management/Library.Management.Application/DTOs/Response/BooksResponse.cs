namespace Library.Management.Application.DTOs.Response
{
    public class BooksResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public long ISBN { get; set; }

        public string? Genre { get; set; }

        public bool IsActive { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    }
}
