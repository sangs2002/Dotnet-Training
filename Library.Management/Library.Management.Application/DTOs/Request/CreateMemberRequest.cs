namespace Library.Management.Application.DTOs.Request
{
    public class CreateMemberRequest
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;


        [Required]
        [EnumDataType(typeof(MemberShipType))]
        public MemberShipType MembershipType { get; set; } 

        public bool IsActive { get; set; }

    }
}
