using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidCalculatorLibrary
{
    public interface IAuctionable
    {
        public AuctionTypesEnum AuctionType { get; set; }
        public decimal BidAmount { get; set; }
    }
}
