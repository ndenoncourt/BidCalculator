using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class PercentCapedFee : PercentFee
    {
        public decimal MinFee { get; set; } = 0;
        public decimal MaxFee { get; set; } = 0;
        public override decimal Calculate(decimal baseAmount)
        {
            decimal fee = base.Calculate(baseAmount);
            if (fee < MinFee) {
                fee = MinFee;
            } else if (fee > MaxFee) {
                fee = MaxFee;
            }
            return fee;
        }
    }
}
