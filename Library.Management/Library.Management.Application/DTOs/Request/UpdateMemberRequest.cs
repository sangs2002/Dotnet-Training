namespace Library.Management.Application.DTOs.Request
{
    public class UpdateMemberRequest
    {
        public int MemberId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public MemberShipType MembershipType { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
