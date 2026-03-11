namespace Library.Management.Application.DTOs.Request
{
    public class CreateBookRequest
    {

        [Required]
        public string Title { get; set; } = null!;

        [Required]

        public string Author { get; set; } = null!;

        [Required]

        public long ISBN { get; set; }

        [Required]
        public string Genre { get; set; }= null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; }
    }
}
