using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public interface ICalculatable
    {
        public FeeTypesEnum FeeType { get; set; }
        public decimal Calculate(decimal baseAmount);
    }
}
