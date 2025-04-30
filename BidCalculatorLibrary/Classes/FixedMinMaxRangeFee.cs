using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class FixedMinMaxRangeFee : FixedMinRangeFee
    {
        public decimal MaxRange { get; set; } = 0;
        public override decimal Calculate(decimal baseAmount)
        {
            decimal fee = 0;
            if (baseAmount <= this.MaxRange) {
                fee = base.Calculate(baseAmount);
            }
            return fee;
        }
    }
}
