using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class FixedFee : BaseFee
    {
        public decimal FeeAmount { get; set; } = 0;
        public override decimal Calculate(decimal baseAmount)
        {
            return FeeAmount;
        }
    }
}
