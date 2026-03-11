namespace Library.Management.Application.DTOs.Response
{
    public class ReturnBookResponse
    {
        public int BookId { get; private set; }

        public int MemberId { get; set; }

        public string Title { get; private set; } = null!;

        public string Name { get; set; } = null!;
    }
}
