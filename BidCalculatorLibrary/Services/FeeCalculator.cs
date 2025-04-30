using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class FeeCalculator : IFeeCalculator
    {
        public List<ICalculatable> FeeList { get; set; } = new List<ICalculatable>();

        public Dictionary<FeeTypesEnum, decimal> CalculateFees(IAuctionable auctionedItem)
        {
            Dictionary<FeeTypesEnum, decimal> FeeAmounts = new Dictionary<FeeTypesEnum, decimal>();
            foreach (ICalculatable fee in FeeList) {
                if (!FeeAmounts.ContainsKey(fee.FeeType)) {
                    FeeAmounts[fee.FeeType] = 0;
                }
                FeeAmounts[fee.FeeType] += fee.Calculate(auctionedItem.BidAmount);
            }
            return FeeAmounts;
        }
    }
}
