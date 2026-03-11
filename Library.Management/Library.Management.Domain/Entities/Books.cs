namespace Library.Management.Domain.Entities
{
    public class Books
    {
        public int BookId { get; private set; }

        public string Title { get;private set; } = null!;

        public string Author { get; private set; } = null!;

        public long ISBN { get; private set; }

        public string Genre { get;private set; }=null!;

        public bool IsActive { get; private set; }

        public int TotalCopies { get; private set; }

        public int AvailableCopies { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public virtual ICollection<Borrowing> Borrowings { get; private set; } = new List<Borrowing>();


        private Books() { }


        public Books(string title, string author, long isbn, int totalCopies, string genre)
        {
            Title = title;
            Author = author;
            Genre = genre;
            ISBN = isbn;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
            IsActive = true;
        }

        public void DecreaseCopies()
        {
            if (AvailableCopies <= 0)
                throw new InvalidOperationException("No copies available");

            AvailableCopies--;
        }

        public void ReturnCopy()
        {
            if (AvailableCopies >= TotalCopies)
                throw new InvalidOperationException("All copies already returned");

            AvailableCopies++;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void UpdateBook(string title,string author,long isbn,string genre,bool isActive,int totalCopies, int availablecopies)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Genre = genre;
            IsActive = isActive;
            TotalCopies = totalCopies;
            AvailableCopies = availablecopies;
        }
    }
}
