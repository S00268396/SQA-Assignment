using Moq;
using NUnit.Framework;

namespace SQA_Week10
{
    //Get the discount Percentage - 0.1 = 10%
    public interface IDiscountService
    {
        double GetDiscount();
    }

    //Calculates parking fees based on hours and the vehicle type
    public class ParkingService
    {
        //Get the discount
        private readonly IDiscountService _discountService;

        //Constructor to set discount service
        public ParkingService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        //Returns total parking fee after applying the hours, vehicleType and discount
        public double CalculateFee(int hours, string vehicleType)
        {
            //Parking fee
            double fee;

            //Calculate parking fee based on vehicle type and hours
            if (vehicleType == "standard") 
            {
                //Standard vehicle pricing
                if ((hours >= 1) && (hours <= 3))
                {
                    //1 - 3 hours: £4 per hour
                    fee = hours * 4.0;
                }
                else if (hours >= 4)
                {
                    //4 or more hours: £3 per hour
                    fee = hours * 3.0;
                }
                else
                {
                    //Less than 1 hour: no charge
                    fee = 0.0;

                }
            }
            else if (vehicleType == "electric")
            {
                //Electric vehicle pricing
                if ((hours >= 1) && (hours <= 5))
                {
                    //1 - 5 hours: £3 per hour
                    fee = hours * 3.0;
                }
                else if (hours >= 6)
                {
                    //6 or more hours: £2 per hour
                    fee = hours * 2.0;
                }
                else
                {
                    //Less than 1 hour: no charge
                    fee = 0.0;
                }
            }
            else
            {
                //Unknown vehicle type: no charge
                fee = 0.0;

            }

            //Retrieve the current discount rate from the discount service
            double discount = _discountService.GetDiscount();

            //Apply discount if the parking duration is 10 hours or more
            if (hours >= 10)
            {
                //Reduce the fee by the discount percentage
                fee = fee - (fee * discount);
            }

            //Return the final parking fee
            return fee;           
        }
    }

    //Tests for the ParkService
    [TestFixture]
    public class Tests
    {
        //Standard_ReturnsTests
        [Test]
        public void TenHour_Standarad_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the standard vehicle parked for 10 hours
            double fee = parkingService.CalculateFee(10, "standard");
             
            //Assert
            //Verify that the fee is correctly calculated with discount applied
            //Standard fee: 10 hours * £3 hours = £30
            //Apply 10% discount: 30 - (30 * 0.1) = £27
            Assert.That(fee, Is.EqualTo(27));
        }                   

        [Test]
        public void ThreeHour_Standard_Returns()
        {
            // Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the standard vehicle parked for 3 hours
            double fee = parkingService.CalculateFee(3, "standard");

            //Assert
            //Standard fee: 3 hours * £4 hours = £12
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(12));
        }

        [Test]
        public void SevenHour_Standard_Returns()
        {
            // Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the standard vehicle parked for 7 hours
            double fee = parkingService.CalculateFee(7, "standard");

            //Assert
            //Standard fee: 7 hours * £3 hours = £9
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(21)); 
        }

        //Electric - Returns
        [Test]
        public void ThreeHour_Electric_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the electric vehicle parked for 3 hours
            double fee = parkingService.CalculateFee(3, "electric");

            //Assert
            //Electric fee: 3 hours * £3 hours = £9
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(9));
        }

        [Test]
        public void SevenHour_Electric_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the electric vehicle parked for 7 hours
            double fee = parkingService.CalculateFee(7, "electric");

            //Assert
            //Electric fee: 7 hours * £2 hours = £14
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(14));
        }

        [Test]
        public void TenHour_Electric_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the electric vehicle parked for 10 hours
            double fee = parkingService.CalculateFee(10, "electric");

            //Assert
            //Electric fee: 10 hours * £2 hours = £20
            //Apply 10% discount: 20 - (20 * 0.1) = £18
            Assert.That(fee, Is.EqualTo(18)); 
        }

        //Invalid Vehicles
        [Test]
        public void ThreeHour_InvalidVehicle_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);


            //Act
            //Calculate the parking fee for the invalid vehicle "Truck" parked for 3 hours
            double fee = parkingService.CalculateFee(3, "Truck");

            //Assert
            //Invalid vehicle fee: £0
            //Discount does not apply - only the standard and electric vehicles with 10+ hour get the discount
            Assert.That(fee, Is.EqualTo(0));
        }

        [Test]
        public void InvalidHour_Electric_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the electric vehicle parked for invalid hour "-1"
            double fee = parkingService.CalculateFee(-1, "electric");

            //Assert
            //Electric fee: -1 hours * £0 hours = £0
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(0));
        }

        [Test]
        public void InvalidHourStandard_Returns()
        {
            //Arrange
            //Create a mock for the IDiscountService
            var mockDiscountService = new Mock<IDiscountService>();

            //Set up the mock to return a 10% discount
            mockDiscountService.Setup(x => x.GetDiscount()).Returns(.1);

            //Create the ParkingService with the mocked discount service
            var parkingService = new ParkingService(mockDiscountService.Object);

            //Act
            //Calculate the parking fee for the standard vehicle parked for invalid hour "-1"
            double fee = parkingService.CalculateFee(-1, "standard");


            //Assert
            //Standard fee: -1 hours * £0 hours = £0
            //Discount does not apply
            Assert.That(fee, Is.EqualTo(0));
        }
    }
}