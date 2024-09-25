using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Repository;


namespace CarAuctionManagementSystem.Inventory
{
    public class AuctionRepository : IAuctionRepository
    {
        public Dictionary<int/*vehicle ID*/, Auction> Inventory { get; set; }

        public AuctionRepository()
        {
            Inventory = new Dictionary<int, Auction>();
        }

        public IEnumerable<Auction> GetAll()
        {
            return Inventory.Values;
        }


        public void Insert(Auction auction)
        {
            Inventory.Add(auction.Vehicle.Id, auction);

        }

        public void Update(Auction auction)
        {
            int key = auction.Vehicle.Id;
            if (Inventory.ContainsKey(key))
            {
                Inventory[key] = auction;
            }

        }

        public Auction GetByVehicleId(int id)
        {

            if (Inventory.ContainsKey(id))
                return Inventory[id];

            return null;
        }

        public Auction GetById(int id)
        {
            var allAuctions = GetAll();
            return allAuctions.FirstOrDefault(a => a.Id == id);
        }
    }
}
