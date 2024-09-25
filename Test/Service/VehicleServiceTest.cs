using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;
using CarAuctionManagementSystem.Repository;
using CarAuctionManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystemTest.Service
{
    public class VehicleServiceTest
    {

        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly VehicleService? _vehicleService;
        Dictionary<string, object> vehiclesArgs1;
        Dictionary<string, object> vehiclesArgs2;

        public VehicleServiceTest()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _vehicleService = new VehicleService( _mockVehicleRepository.Object);

            vehiclesArgs1 = new Dictionary<string, object>();
            vehiclesArgs1.Add("id", 1);
            vehiclesArgs1.Add("type", "Hatchback");
            vehiclesArgs1.Add("manufacturer", "Toyota");
            vehiclesArgs1.Add("model", "Toyota");
            vehiclesArgs1.Add("year", 2001);
            vehiclesArgs1.Add("startingBid", 100);
            vehiclesArgs1.Add("numberOfDors", 5);

            vehiclesArgs2 = new Dictionary<string, object>();
            vehiclesArgs2.Add("id", 2);
            vehiclesArgs2.Add("manufacturer", "Toyota");
            vehiclesArgs2.Add("model", "Toyota");
            vehiclesArgs2.Add("year", 2001);
            vehiclesArgs2.Add("startingBid", 100);
            vehiclesArgs2.Add("numberOfDors", 5);
        }

        [Fact(DisplayName ="Add Vehicle When Id is valid returns Sucess")]
        public void AddVehicle_WhenIdIsValid_returnsSucess()
        {
            //Arrange
            int vehicleId = 1;
            string operationMessage = $"The Vehicle with the ID {vehicleId} was successfuly inserted";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(null as Vehicle );

            //Act
            var result = _vehicleService.AddVehicle(vehiclesArgs1);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockVehicleRepository.Verify(x => x.Insert(It.Is<Vehicle>(a => a.Id == vehicleId)),Times.Once);

        }

        [Fact(DisplayName = "Add Vehicle When Id is not valid returns Failure")]
        public void AddVehicle_WhenIdIsNotValid_returnsFailure()
        {
            //Arrange
            int vehicleId = 1;
            var vehicle = new Hatchback(vehiclesArgs1);
            string operationMessage = $"Error while inserting, a Vehicle with the ID {vehicleId} already exist";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle);

            //Act
            var result = _vehicleService.AddVehicle(vehiclesArgs1);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockVehicleRepository.Verify(x => x.Insert(It.Is<Vehicle>(a => a.Id == vehicleId)),Times.Never);

        }

        [Fact(DisplayName ="GetByModel_WhenModelIsFound_ReturnVehicles")]
        public void GetByModel_WhenModelIsFound_ReturnVehicles()
        {
            //Arrange
            string model = "Toyota";
            var vehicle1=new Hatchback(vehiclesArgs1);
            var vehicle2=new Hatchback(vehiclesArgs2);
            var vehicles = new List<Vehicle>();
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            _mockVehicleRepository.Setup(x => x.GetByModel(model)).Returns(vehicles.Where(v => v.Model.Equals(model)));

            //Act
            var result= _vehicleService.GetByModel(model);

            //Assert
            Assert.True(result.Success);
            Assert.True(vehicles.Count==result.Data.Count());
        }

        [Fact(DisplayName = "GetByType_WhenModelIsFound_ReturnVehicles")]
        public void GetByType_WhenTypeIsFound_ReturnVehicles()
        {
            //Arrange
            string _type = "Hatchback";
            var vehicle1 = new Hatchback(vehiclesArgs1);
            var vehicle2 = new Hatchback(vehiclesArgs2);
            var vehicles = new List<Vehicle>();
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            _mockVehicleRepository.Setup(x => x.GetByType(_type)).Returns(vehicles.Where(v => v.Type.Equals(_type)));

            //Act
            var result = _vehicleService.GetByType(_type);

            //Assert
            Assert.True(result.Success);
            Assert.True(vehicles.Count == result.Data.Count());
        }

        [Fact(DisplayName = "GetByYear_WhenModelIsFound_ReturnVehicles")]
        public void GetByYear_WhenModelIsFound_ReturnVehicles()
        {
            //Arrange
            int year = 2001;
            var vehicle1 = new Hatchback(vehiclesArgs1);
            var vehicle2 = new Hatchback(vehiclesArgs2);
            var vehicles = new List<Vehicle>();
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            _mockVehicleRepository.Setup(x => x.GetByYear(year)).Returns(vehicles.Where(v => v.Year.Equals(year)));

            //Act
            var result = _vehicleService.GetByYear(year);

            //Assert
            Assert.True(result.Success);
            Assert.True(vehicles.Count == result.Data.Count());
        }

        [Fact(DisplayName = "GetByManufacturer_WhenModelIsFound_ReturnVehicles")]
        public void GetByManufacturer_WhenModelIsFound_ReturnVehicles()
        {
            //Arrange
            string manufacturer = "Toyota";
            var vehicle1 = new Hatchback(vehiclesArgs1);
            var vehicle2 = new Hatchback(vehiclesArgs2);
            var vehicles = new List<Vehicle>();
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            _mockVehicleRepository.Setup(x => x.GetByManufacturer(manufacturer)).Returns(vehicles.Where(v => v.Manufacturer.Equals(manufacturer)));

            //Act
            var result = _vehicleService.GetByManufacturer(manufacturer);

            //Assert
            Assert.True(result.Success);
            Assert.True(vehicles.Count == result.Data.Count());
        }
    }
}
