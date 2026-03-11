namespace Library.Management.Application.Factory
{
    public class MemberFactory : IMemberFactory
    {

        public IMemberBase Create(MemberShipType membershipType, int memberId)
        {
            return membershipType switch
            {
                MemberShipType.Premium => new PremiumMember(memberId),
                MemberShipType.Regular => new RegularMember(memberId),
                _ => throw new ArgumentException("Invalid membership type")
            };
        }
    }
}
