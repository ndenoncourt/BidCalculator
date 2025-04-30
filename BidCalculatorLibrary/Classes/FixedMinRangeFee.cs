using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class FixedMinRangeFee: FixedFee
    {
        public decimal MinRange { get; set; } = 0;
        public override decimal Calculate(decimal baseAmount)
        {
            decimal fee = 0;
            if (baseAmount >= this.MinRange) {
                fee = this.FeeAmount;
            }
            return fee;
        }
    }
}
