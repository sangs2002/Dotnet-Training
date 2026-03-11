namespace Library.Management.Application.Strategies.MembershipType
{
    public class PremiumFineCalculator : IFineCalculator
    {
        public decimal Calculate(int lateDays)
        {
           return lateDays * MemberConstants.PreminumFineAmount;
        }
    }
}
