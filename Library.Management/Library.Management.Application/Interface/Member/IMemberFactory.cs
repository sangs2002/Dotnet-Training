namespace Library.Management.Application.Interface.Member
{
    public interface IMemberFactory
    {
        IMemberBase Create(MemberShipType membershipType, int memberId);

    }
}
