using CarAuctionManagementSystem.Inventory;
using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;
using CarAuctionManagementSystem.Repository;
using CarAuctionManagementSystem.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystemTest.Service
{
    public class AuctionServiceTest
    {
        private readonly Mock<IAuctionRepository> _mockAuctionRepository;
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly AuctionService _auctionService;
        Dictionary<string, object> vehiclesArgs1;
        Dictionary<string, object> vehiclesArgs2;
        Vehicle vehicle1;
        Vehicle vehicle2;
        List<Vehicle> vehicles;
        Auction auction;

        public AuctionServiceTest()
        {
            _mockAuctionRepository = new Mock<IAuctionRepository>();
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _auctionService = new AuctionService(_mockAuctionRepository.Object, _mockVehicleRepository.Object);



            vehiclesArgs1 = new Dictionary<string, object>();
            vehiclesArgs1.Add("id", 1);
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

            vehicle1 = new Hatchback(vehiclesArgs1);
            vehicle2 = new Sedan(vehiclesArgs2);
            vehicles = new List<Vehicle>();
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            auction = new Auction(vehicle1);
        }

        #region CloseAuction
        [Fact(DisplayName ="CloseAuction returns sucess when Auction is Valid")]
        public void CloseAuction_WhenAuctionIsValid_ReturnsSuccess()
        {
            //Arrange 
            auction.Active = true;
            int vehicleId = 1;
            string operationMessage = $"Auction for the Vehicle {vehicleId} is closed";
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);


            //Act
            var result = _auctionService.CloseAuction(vehicleId);


            //Assert
            Assert.True(result.Success);
            Assert.Equal(operationMessage, result.Message);
            //verify if the update was called once
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == false)),Times.Once);
        }

        [Fact(DisplayName ="CloseAuction When vehicle Auction does not exist returns failure ")]
        public void CloseAuction_WhenVehicleAuctionNotFound_ReturnsFailure()
        {
            //Arrange 
            auction.Active = true;
            int vehicleId = 1;
            string operationMessage = $"The Vehicle Has no Existing Auction";
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(null as Auction);


            //Act
            var result = _auctionService.CloseAuction(vehicleId);


            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            //verify if the update was called once
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == false)), Times.Never);
        }

        [Fact(DisplayName ="Close Auction When vehicle Auction is closed return Failure")]
        public void CloseAuction_WhenVehicleAuctionClosed_ReturnsFailure()
        {
            //Arrange 
            auction.Active = false;
            int vehicleId = 1;
            string operationMessage = $"Auction for the Vehicle {vehicleId} is already closed";
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);


            //Act
            var result = _auctionService.CloseAuction(vehicleId);


            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            //verify if the update was called once
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == false)), Times.Never);
        }
        #endregion


        #region StartAuction
        [Fact(DisplayName ="StartAuction When existing Auction is valid returns sucess")]
        public void StartAuction_WhenExistingAuctionIsValid_ReturnsSucess()
        {
            //Arrange 
            auction.Active = false;
            int vehicleId = 1;
            string operationMessage = $"Auction for the Vehicle {vehicleId} has Started whith the starting bid of {auction.Bid}";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);

            //Act
            var result = _auctionService.StartAuction(vehicleId);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockVehicleRepository.Verify(r => r.GetById(It.Is<int>(a => a == vehicleId)), Times.Once);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Once);
            _mockAuctionRepository.Verify(r => r.Insert(It.Is<Auction>(a => a.Active == true)), Times.Never);


        }

        [Fact(DisplayName ="StartAuction when no existing Auction returns Success ")]
        public void StartAuction_WhenNoExisingAuction_returnTrue()
        {
            //Arrange 
            auction.Active = false;
            int vehicleId = 1;
            string operationMessage = $"Auction for the Vehicle {vehicleId} has Started whith the starting bid of {auction.Bid}";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(null as Auction);

            //Act
            var result = _auctionService.StartAuction(vehicleId);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockVehicleRepository.Verify(r => r.GetById(It.Is<int>(a=>a == vehicleId)),Times.Once);
            _mockAuctionRepository.Verify(r => r.Insert(It.Is<Auction>(a => a.Active == true)), Times.Once);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);

        }

        [Fact(DisplayName ="StartAuction_WhenNoExistingVehicle_ReturnsFailure")]
        public void StartAuction_WhenNoExistingVehicle_ReturnsFailure()
        {
            //Arrange
            auction.Active = false;
            int vehicleId = 1;
            string operationMessage = $"The Vehicle with the ID {vehicleId} does not exist";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(null as Vehicle);


            //Act
            var result= _auctionService.StartAuction(vehicleId);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Insert(It.Is<Auction>(a => a.Active == true)), Times.Never);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);
        }

        [Fact(DisplayName ="StartAuction_WhenVehicleAuctionStarted_retunrsFailure")]
        public void StartAuction_WhenVehicleAuctionStarted_retunrsFailure()
        {
            //Arrange
            auction.Active = true;
            int vehicleId = 1;
            string operationMessage = $" It is not possible to start Auction for the vehicle {vehicleId} it already has an Active auction";
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);


            //Act
            var result = _auctionService.StartAuction(vehicleId);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Insert(It.Is<Auction>(a => a.Active == true)), Times.Never);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);
        }
        #endregion

        #region PlaceBid

        [Fact(DisplayName ="PlaceBid When Existing Vehicle And Valid Bid returns Success")]
        public void PlaceBid_WhenExistingVehicleAndValidBid_ReturnSuccess()
        {
            //Arrange
            int bid = 101;
            int vehicleId = 1;
            string operationMessage = $"The Bid for the Vehicle {vehicleId} has been updated";
            auction.Active = true;
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);


            //Act
            var result = _auctionService.PlaceBid(vehicleId,bid);


            //Assert
            Assert.True(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Once);

        }

        [Fact(DisplayName ="PlaceBid When No Existing Vehicle returs failure")]
        public void PlaceBid_WhenNoExistingVehicle_RetursFailure()
        {
            //Arrange
            int bid = 101;
            int vehicleId = 1;
            string operationMessage = $"The Vehicle does not exist";
            auction.Active = true;
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(null as Vehicle);


            //Act
            var result = _auctionService.PlaceBid(vehicleId, bid);


            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);
        }

        [Fact(DisplayName = "PlaceBid When Existing Bid greater then Bid returs failure")]
        public void PlaceBid_WhenExistingBidGreaterThenNewBid_RetursFailure()
        {
            //Arrange
            int bid = 99;
            int vehicleId = 1;
            string operationMessage = $"The existing Bid is greater then {bid}. Consider bidding higher";
            auction.Active = true;
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(auction);


            //Act
            var result = _auctionService.PlaceBid(vehicleId, bid);


            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);
        }

        [Fact(DisplayName ="PlaceBid When Vehicle Has No Existing Auction Returns Failure")]
        public void PlaceBid_WhenVehicleHasNoExistingAuction_ReturnsFailure()
        {
            //Arrange
            int bid = 99;
            int vehicleId = 1;
            string operationMessage = $"The Vehicle Has no Active Auction";
            auction.Active = true;
            _mockVehicleRepository.Setup(x => x.GetById(vehicleId)).Returns(vehicle1);
            _mockAuctionRepository.Setup(x => x.GetByVehicleId(vehicleId)).Returns(null as Auction);


            //Act
            var result = _auctionService.PlaceBid(vehicleId, bid);


            //Assert
            Assert.False(result.Success);
            Assert.Equal(operationMessage, result.Message);
            _mockAuctionRepository.Verify(r => r.Update(It.Is<Auction>(a => a.Active == true)), Times.Never);
        }
        #endregion
    }
}
