using BidCalculatorLibrary;

namespace BidCalculatorTest
{
    [TestClass]
    public sealed class FeeCalculationTests
    {
        [TestMethod]
        public void TestFixedFee()
        {
            ICalculatable fee = new FixedFee() { FeeType = FeeTypesEnum.Storage, FeeAmount = 100 };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 100);
            Assert.IsTrue(fee.Calculate(501) == 100);
            Assert.IsTrue(fee.Calculate(57) == 100);
            Assert.IsTrue(fee.Calculate(1800) == 100);
            Assert.IsTrue(fee.Calculate(1100) == 100);
            Assert.IsTrue(fee.Calculate(1000000) == 100);
        }
        [TestMethod]
        public void TestFixedMinRangeFee()
        {
            ICalculatable fee = new FixedMinRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 3001, FeeAmount = 20 };

            // Testing MinRange
            Assert.IsTrue(fee.Calculate(3000) == 0);
            Assert.IsTrue(fee.Calculate(3001) == 20);

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 0);
            Assert.IsTrue(fee.Calculate(501) == 0);
            Assert.IsTrue(fee.Calculate(57) == 0);
            Assert.IsTrue(fee.Calculate(1800) == 0);
            Assert.IsTrue(fee.Calculate(1100) == 0);
            Assert.IsTrue(fee.Calculate(1000000) == 20);
        }
        [TestMethod]
        public void TestFixedMinMaxRangeFee()
        {
            ICalculatable fee = new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1, MaxRange = 500, FeeAmount = 5 };

            // Testing MinRange
            Assert.IsTrue(fee.Calculate(0) == 0);
            Assert.IsTrue(fee.Calculate(1) == 5);

            // Testing MaxRange
            Assert.IsTrue(fee.Calculate(500) == 5);
            Assert.IsTrue(fee.Calculate(501) == 0);

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 5);
            Assert.IsTrue(fee.Calculate(501) == 0);
            Assert.IsTrue(fee.Calculate(57) == 5);
            Assert.IsTrue(fee.Calculate(1800) == 0);
            Assert.IsTrue(fee.Calculate(1100) == 0);
            Assert.IsTrue(fee.Calculate(1000000) == 0);

            fee = new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 501, MaxRange = 1000, FeeAmount = 10 };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 0);
            Assert.IsTrue(fee.Calculate(501) == 10);
            Assert.IsTrue(fee.Calculate(57) == 0);
            Assert.IsTrue(fee.Calculate(1800) == 0);
            Assert.IsTrue(fee.Calculate(1100) == 0);
            Assert.IsTrue(fee.Calculate(1000000) == 0);

            fee = new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1001, MaxRange = 3000, FeeAmount = 15 };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 0);
            Assert.IsTrue(fee.Calculate(501) == 0);
            Assert.IsTrue(fee.Calculate(57) == 0);
            Assert.IsTrue(fee.Calculate(1800) == 15);
            Assert.IsTrue(fee.Calculate(1100) == 15);
            Assert.IsTrue(fee.Calculate(1000000) == 0);
        }
        [TestMethod]
        public void TestPercentFee()
        {
            ICalculatable fee = new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.02m };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 7.96m);
            Assert.IsTrue(fee.Calculate(501) == 10.02m);
            Assert.IsTrue(fee.Calculate(57) == 1.14m);
            Assert.IsTrue(fee.Calculate(1100) == 22m);

            fee = new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.04m };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(1800) == 72m);
            Assert.IsTrue(fee.Calculate(1000000) == 40000m);
        }
        [TestMethod]
        public void TestPercentCappedFee()
        {
            ICalculatable fee = new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 10, MaxFee = 50 };

            //Testing MinFee
            Assert.IsTrue(fee.Calculate(99m) == 10m);
            Assert.IsTrue(fee.Calculate(101m) == 10.1m);

            //Testing MaxFee
            Assert.IsTrue(fee.Calculate(499m) == 49.9m);
            Assert.IsTrue(fee.Calculate(501m) == 50m);

            // Testing examples given
            Assert.IsTrue(fee.Calculate(398) == 39.8m);
            Assert.IsTrue(fee.Calculate(501) == 50m);
            Assert.IsTrue(fee.Calculate(57) == 10m);
            Assert.IsTrue(fee.Calculate(1100) == 50m);

            fee = new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 25, MaxFee = 200 };

            // Testing examples given
            Assert.IsTrue(fee.Calculate(1800) == 180m);
            Assert.IsTrue(fee.Calculate(1000000) == 200m);
        }
        [TestMethod]
        public void TestFeeCalculator()
        {
            List<Dictionary<FeeTypesEnum, decimal>> expectedFees = new List<Dictionary<FeeTypesEnum, decimal>>() {
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 39.8m },{ FeeTypesEnum.Special, 7.96m },{ FeeTypesEnum.Association, 5m },{ FeeTypesEnum.Storage, 100m } },
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 50m },{ FeeTypesEnum.Special, 10.02m },{ FeeTypesEnum.Association, 10m },{ FeeTypesEnum.Storage, 100m } },
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 10m },{ FeeTypesEnum.Special, 1.14m },{ FeeTypesEnum.Association, 5m },{ FeeTypesEnum.Storage, 100m } },
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 180m },{ FeeTypesEnum.Special, 72m },{ FeeTypesEnum.Association, 15m },{ FeeTypesEnum.Storage, 100m } },
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 50m },{ FeeTypesEnum.Special, 22m },{ FeeTypesEnum.Association, 15m },{ FeeTypesEnum.Storage, 100m } },
                 new Dictionary<FeeTypesEnum, decimal>() { { FeeTypesEnum.Basic, 200m },{ FeeTypesEnum.Special, 40000m },{ FeeTypesEnum.Association, 20m },{ FeeTypesEnum.Storage, 100m } }
            };

            List<Dictionary<FeeTypesEnum, decimal>> resultFees = new List<Dictionary<FeeTypesEnum, decimal>>();

            List<Vehicle> vehicles = new List<Vehicle>() {
                new Vehicle() { AuctionType = AuctionTypesEnum.Common, BidAmount = 398m },
                new Vehicle() { AuctionType = AuctionTypesEnum.Common, BidAmount = 501m },
                new Vehicle() { AuctionType = AuctionTypesEnum.Common, BidAmount = 57m },
                new Vehicle() { AuctionType = AuctionTypesEnum.Luxury, BidAmount = 1800m },
                new Vehicle() { AuctionType = AuctionTypesEnum.Common, BidAmount = 1100m },
                new Vehicle() { AuctionType = AuctionTypesEnum.Luxury, BidAmount = 1000000m },
            };
            decimal total = 0;

            foreach (Vehicle vehicle in vehicles) {
                total = 0;
                Dictionary<FeeTypesEnum, decimal> feeAmounts = new Dictionary<FeeTypesEnum, decimal>();

                IFeeCalculator feeCalculator = new FeeCalculator();
                switch (vehicle.AuctionType) {
                    case AuctionTypesEnum.Common:
                        feeCalculator = FeeCalculationTests.GetCommonFeeCalculator();
                        break;
                    case AuctionTypesEnum.Luxury:
                        feeCalculator = FeeCalculationTests.GetLuxuryFeeCalculator();
                        break;
                }

                feeAmounts = feeCalculator.CalculateFees(vehicle);
                resultFees.Add(feeAmounts);
            }

            for (int i = 0; i < expectedFees.Count; i++) {
                CollectionAssert.AreEqual(expectedFees[i],resultFees[i]);
            }
        }
        static IFeeCalculator GetCommonFeeCalculator()
        {
            return new FeeCalculator() {
                FeeList = new List<ICalculatable>() {
                    new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 10m, MaxFee = 50m },
                    new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.02m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1m, MaxRange = 500m, FeeAmount = 5m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 501m, MaxRange = 1000m, FeeAmount = 10m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1001m, MaxRange = 3000m, FeeAmount = 15m },
                    new FixedMinRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 3001m, FeeAmount = 20m },
                    new FixedFee() { FeeType = FeeTypesEnum.Storage, FeeAmount = 100m }
                }
            };
        }
        static IFeeCalculator GetLuxuryFeeCalculator()
        {
            return new FeeCalculator() {
                FeeList = new List<ICalculatable>() {
                    new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 25m, MaxFee = 200m },
                    new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.04m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1m, MaxRange = 500m, FeeAmount = 5m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 501m, MaxRange = 1000m, FeeAmount = 10m },
                    new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1001m, MaxRange = 3000m, FeeAmount = 15m },
                    new FixedMinRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 3001m, FeeAmount = 20m },
                    new FixedFee() { FeeType = FeeTypesEnum.Storage, FeeAmount = 100m }
                }
            };
        }
    }
}
