namespace Library.Management.Application.Factory.Members
{
    public class PremiumMember : IMemberBase
    {
        public PremiumMember(int memberId) : base(memberId) { }

        public override int MaxBooksAllowed => MemberConstants.PreminumBookLimit;
    }
}
