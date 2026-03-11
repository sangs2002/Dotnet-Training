namespace Library.Management.Application.DTOs.Response
{
    public class MemberResponse
    {
        public int MemberId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberShipType MembershipType { get; set; } 

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
        public virtual ICollection<Fines> Fines { get; set; } = new List<Fines>();
    }
}
