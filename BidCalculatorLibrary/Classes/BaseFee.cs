using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public abstract class BaseFee : ICalculatable
    {
        public FeeTypesEnum FeeType { get; set; } = FeeTypesEnum.None;

        public abstract decimal Calculate(decimal baseAmount);
    }
}
