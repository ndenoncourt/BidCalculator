using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class PercentFee : BaseFee
    {
        public decimal Percent { get; set; } = 0;
        public override decimal Calculate(decimal baseAmount)
        {
            return baseAmount * this.Percent;
        }
    }
}
