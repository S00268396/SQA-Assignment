using Moq;
using NUnit.Framework;

namespace SQA_Week10
{
    public interface IDiscountService
    {
        double GetDiscount();
    }
    public class ParkingService
    {
        private readonly IDiscountService _discountService;

        public ParkingService(IDiscountService discountService)
        {
            _discountService = discountService;
        }


        public double CalculateFee(int hours, string vehicleType)
        {
            double fee;

            if (vehicleType == "standard")
                if ((hours >= 1) && (hours < 3))
                    fee = hours * 4.0;
                else if (hours >= 4)
                    fee = hours * 3.0;
                else
                    fee = 0.0;
            else if (vehicleType == "electric")
                if ((hours >= 1) && (hours <= 5))
                    fee = hours * 3.0;
                else if (hours >= 6)
                    fee = hours * 2.0;
                else
                    fee = 0.0;
            else
                fee = 0.0;

            double discount = _discountService.GetDiscount();

            if (hours >= 10)
                fee = fee - (fee * discount);

            return fee;
        }
    }

    [TestFixture]

    public class Tests
    {


        [Test]
        public void GetParkingFee_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(10, "standard");

            // Assert
            //Assert.AreEqual("It's hot!", weatherDescription); // classic
            Assert.That(fee, Is.EqualTo(27)); // constraint
        }
    }
}