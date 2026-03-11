namespace Library.Management.Application.DTOs.Response
{
    public class BorrowResponse
    {
        public int BorrowingId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }

        public DateOnly BorrowedDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnedDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BorrowStatus Status { get; set; }


    }
}
