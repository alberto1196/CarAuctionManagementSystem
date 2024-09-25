using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystemTest.Model
{
    public class VehicleTest
    {
        Dictionary<string, object> vehiclesArgs1;
        public VehicleTest()
        {
            vehiclesArgs1 = new Dictionary<string, object>();
            vehiclesArgs1.Add("id", 1);
            vehiclesArgs1.Add("type", "Hatchback");
            vehiclesArgs1.Add("manufacturer", "Toyota");
            vehiclesArgs1.Add("model", "Toyota");
            vehiclesArgs1.Add("year", 2001);
            vehiclesArgs1.Add("startingBid", 100);
            vehiclesArgs1.Add("numberOfDors", 5);
        }

        [Fact(DisplayName ="NewVehicle_WhenArgumentIsValid_CreatesSuccefuly")]
        public void NewVehicle_WhenArgumentIsValid_CreatesSuccefuly()
        {
            //Arrange&Act
            Vehicle vehicle = new Hatchback(vehiclesArgs1);

            //Assert
            Assert.Equal(vehicle.Id, vehiclesArgs1["id"]);

        }
        [Fact(DisplayName = "NewVehicle When Argument Is NotValid Throws ArgumentException")]
        public void NewVehicle_WhenArgumentIsNotValid_ThrowsArgumentException()
        {
            var args = new Dictionary<string, object>()
            {
                {"id", 1},
                {"type", "type"},
                {"model", "model"},
                {"year",2001 },
                {"manufacturer","manufacturer"  },
                {"startingBid",100 }
            };
            var exceptionMessage = "Number of dors is required";
            //Arrange&Act
            var exception = Assert.Throws<ArgumentException>(() => new Hatchback(args));
            //Assert
            Assert.Equal(exceptionMessage, exception.Message);

        }
    }
}
