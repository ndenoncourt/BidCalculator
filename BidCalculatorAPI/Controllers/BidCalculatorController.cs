using BidCalculatorLibrary;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidCalculatorController : ControllerBase
    {
        private readonly ILogger<BidCalculatorController> _logger;
        private readonly IServiceProvider _provider;

        public BidCalculatorController(ILogger<BidCalculatorController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet(Name = "GetBidCalculation")]
        [HttpPost(Name = "GetBidCalculation")]
        public Dictionary<FeeTypesEnum, decimal> Get(VehicleModel vehicleModel)
        {
            Vehicle vehicle = new Vehicle() {
                AuctionType = vehicleModel.AuctionType,
                BidAmount = vehicleModel.BidAmount,
            };
            IFeeCalculator feeCalculator = _provider.GetKeyedService<IFeeCalculator>((object)vehicle.AuctionType) ?? new FeeCalculator();

            return feeCalculator.CalculateFees(vehicle);
        }
    }
}
