
namespace BidCalculatorLibrary
{
    public interface IFeeCalculator
    {
        List<ICalculatable> FeeList { get; set; }

        Dictionary<FeeTypesEnum, decimal> CalculateFees(IAuctionable auctionedItem);
    }
}