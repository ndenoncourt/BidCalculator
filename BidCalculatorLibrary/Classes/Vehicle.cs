using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public class Vehicle : IAuctionable
    {
        public AuctionTypesEnum AuctionType { get; set; } = AuctionTypesEnum.None;
        public decimal BidAmount { get; set; } = 0;
    }
}
