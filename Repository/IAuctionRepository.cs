using CarAuctionManagementSystem.Models;

namespace CarAuctionManagementSystem.Repository
{
    public interface IAuctionRepository
    {
        IEnumerable<Auction> GetAll();

        Auction? GetByVehicleId(int id);

        Auction GetById(int id);

        void Insert(Auction auction);

        void Update(Auction auction);
    }
}
