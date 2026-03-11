namespace Library.Management.Application.DTOs.Request
{
    public class UpdateBooksRequest
    {

        public int BookId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]

        public string Author { get; set; } = null!;

        [Required]

        public long ISBN { get; set; }

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]

        public int TotalCopies { get; set; }

        [Required]

        public int AvailableCopies { get; set; }
    }
}
