using CarAuctionManagementSystem.Models.Vehicles;
using CarAuctionManagementSystem.Utils;

namespace CarAuctionManagementSystem.Models
{
    public class Auction
    {
        public int Id;
        public bool Active { get; set; }    
        public Vehicle Vehicle { get; set; }
        public double Bid ;

        public Auction( Vehicle vehicle)
        {
            Id = IDGenerator.GetNextAuctionId();
            Vehicle= vehicle;
            Bid = vehicle.StartingBid;
            Active = false;
        }

        
    }
}
