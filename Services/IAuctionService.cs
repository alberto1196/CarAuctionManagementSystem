using CarAuctionManagementSystem.Utils;

namespace CarAuctionManagementSystem.Services
{
    public interface IAuctionService
    {
        OperationResult StartAuction(int vehicleId);
        OperationResult CloseAuction(int vehicleId);

        OperationResult PlaceBid(int vehicleId, double bid);
    }
}
