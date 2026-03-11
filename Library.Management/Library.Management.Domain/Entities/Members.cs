
namespace Library.Management.Domain.Entities
{
    public class Members
    {
        public int MemberId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public MemberShipType MembershipType { get; set; } 

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
        public virtual ICollection<Fines> Fines { get; private set; } = new List<Fines>();



        private Members() { }

        public Members(string name, string email, MemberShipType memberShipType, bool isActive)
        {
            Name = name;
            Email = email;
            MembershipType = memberShipType; 
            IsActive = true;
        }

        public void UpdateBook(string name, string email, MemberShipType memberShipType, bool isActive)
        {
            Name = name;
            Email = email;
            MembershipType = memberShipType;
            IsActive = true;
        }

    }
}
