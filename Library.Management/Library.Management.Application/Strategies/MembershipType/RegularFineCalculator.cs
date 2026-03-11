namespace Library.Management.Application.Strategies.MembershipType
{
    public class RegularFineCalculator : IFineCalculator
    {
        public decimal Calculate(int lateDays)
        {
            return lateDays * MemberConstants.RegularFineAmount;
        }
    }
}
