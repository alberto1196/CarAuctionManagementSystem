using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Repository;
using CarAuctionManagementSystem.Utils;

namespace CarAuctionManagementSystem.Services
{
    public class AuctionService : IAuctionService
    {
        IAuctionRepository _auctionRepository;
        IVehicleRepository _vehicleRepository;

        public AuctionService(IAuctionRepository auctionRepository, IVehicleRepository vehicleRepository)
        {
            _auctionRepository = auctionRepository;
            _vehicleRepository = vehicleRepository;
        }

        public OperationResult CloseAuction(int vehicleId)
        {
            try
            {
                var vehicleAuction = _auctionRepository.GetByVehicleId(vehicleId);
                if (vehicleAuction == null)
                {
                    return new OperationResult { Success = false, Message = $"The Vehicle Has no Existing Auction" };
                }

                if (vehicleAuction.Active == false)
                {
                    return new OperationResult { Success = false, Message = $"Auction for the Vehicle {vehicleId} is already closed" };
                }

                vehicleAuction.Active = false;

                _auctionRepository.Update(vehicleAuction);

                return new OperationResult { Success = true, Message = $"Auction for the Vehicle {vehicleId} is closed" };


            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }


        }

        public OperationResult PlaceBid(int vehicleId, double bid)
        {
            try
            {

                if (_vehicleRepository.GetById(vehicleId) == null)
                {
                    return new OperationResult { Success = false, Message = $"The Vehicle does not exist" };
                }

                var vehicleAuction = _auctionRepository.GetByVehicleId(vehicleId);
                if (vehicleAuction==null || vehicleAuction.Active == false)
                {
                    return new OperationResult { Success = false, Message = $"The Vehicle Has no Active Auction" };
                }

                if (vehicleAuction.Bid >= bid)
                {
                    return new OperationResult { Success = false, Message = $"The existing Bid is greater then {bid}. Consider bidding higher" };
                }


                vehicleAuction.Bid = bid;

                _auctionRepository.Update(vehicleAuction);

                return new OperationResult { Success = true, Message = $"The Bid for the Vehicle {vehicleId} has been updated" };

            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }

        public OperationResult StartAuction(int vehicleId)
        {
            try
            {
                var vehicle = _vehicleRepository.GetById(vehicleId);
                if (vehicle == null)
                {
                    return new OperationResult { Success = false, Message = $"The Vehicle with the ID {vehicleId} does not exist" };
                }

                var vehicleAuction = _auctionRepository.GetByVehicleId(vehicleId);
                if (vehicleAuction == null)
                //no Auction created for this vehicle. so create a new one
                {
                    var newAuction = new Auction(vehicle);
                    newAuction.Active = true;
                    _auctionRepository.Insert(newAuction);
                    return new OperationResult { Success = true, Message = $"Auction for the Vehicle {vehicleId} has Started whith the starting bid of {newAuction.Bid}" };
                }

                if (vehicleAuction.Active == true)
                {
                    return new OperationResult { Success = false, Message = $" It is not possible to start Auction for the vehicle {vehicleId} it already has an Active auction" };
                }

                vehicleAuction.Active = true;
                vehicleAuction.Bid = vehicle.StartingBid;
                _auctionRepository.Update(vehicleAuction);

                return new OperationResult { Success = true, Message = $"Auction for the Vehicle {vehicleId} has Started whith the starting bid of {vehicleAuction.Bid}" };

            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };

            }

        }
    }
}
