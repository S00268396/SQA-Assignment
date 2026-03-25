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
                if ((hours >= 1) && (hours <= 3))
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
        //Standard_ReturnsTests
        [Test]
        public void TenHourStandarad_Returns()
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

        [Test]
        public void ThreeHourStandard_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(3, "standard");

            // Assert
            //Assert.AreEqual("It's hot!", weatherDescription); // classic
            Assert.That(fee, Is.EqualTo(12)); // constraint
        }

        [Test]
        public void SevenHourStandard_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(7, "standard");

            // Assert
            //Assert.AreEqual("It's hot!", weatherDescription); // classic
            Assert.That(fee, Is.EqualTo(21)); // constraint
        }

        //Electric - Returns
        [Test]
        public void ThreeHourElectric_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(3, "electric");

            // Assert
            Assert.That(fee, Is.EqualTo(9)); // constraint
        }

        [Test]
        public void SevenHourElectric_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(7, "electric");

            // Assert
            Assert.That(fee, Is.EqualTo(14)); // constraint
        }

        [Test]
        public void TenHourElectric_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(10, "electric");

            // Assert
            Assert.That(fee, Is.EqualTo(18)); // constraint
        }

        //Invalid Vehicle
        [Test]
        public void ThreeHourInvalidVehicle_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(3, "Truck");

            // Assert
            Assert.That(fee, Is.EqualTo(0)); // constraint
        }
        [Test]
        public void InvalidHourElectric_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(-1, "electric");

            // Assert
            Assert.That(fee, Is.EqualTo(0)); // constraint
        }

        [Test]
        public void InvalidHourStandard_Returns()
        {
            // Arrange
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);
            var parkingService = new ParkingService(mockDiscountService.Object);

            // Act
            double fee = parkingService.CalculateFee(-1, "standard");

            // Assert
            //Assert.AreEqual("It's hot!", weatherDescription); // classic
            Assert.That(fee, Is.EqualTo(0)); // constraint
        }
    }
}