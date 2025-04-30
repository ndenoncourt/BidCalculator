using BidCalculatorLibrary;
using System.ComponentModel.DataAnnotations;
namespace BidCalculatorAPI
{
    public class VehicleModel
    {
        [Required]
        [AllowedValues(AuctionTypesEnum.Common, AuctionTypesEnum.Luxury, ErrorMessage = "Value for field {0} must be Common or Luxury")]
        public AuctionTypesEnum AuctionType { get; set; } = AuctionTypesEnum.None;
        [Required]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal BidAmount { get; set; } = 0;
    }
}
