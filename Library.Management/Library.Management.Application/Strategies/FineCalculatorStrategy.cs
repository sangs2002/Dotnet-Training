
namespace Library.Management.Application.Strategies
{
    public static class FineCalculatorStrategy
    {

        public static IFineCalculator Resolve(MemberShipType type)
        {

            return type switch
            {
                MemberShipType.Premium => new PremiumFineCalculator(),
                _ => new RegularFineCalculator(),

            };
        }
    }
}
