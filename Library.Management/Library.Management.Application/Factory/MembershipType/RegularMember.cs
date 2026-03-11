namespace Library.Management.Application.Factory.Members
{
    public class RegularMember: IMemberBase
    {
        public RegularMember(int memberId) : base(memberId) { }

        public override int MaxBooksAllowed => MemberConstants.RegularBookLimit;
    }
}
