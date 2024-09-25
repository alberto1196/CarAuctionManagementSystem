namespace CarAuctionManagementSystem.Managers
{
    public interface IAuctionManager
    {
        void StartAuction();
        void CloseAuction();

        void PlaceBid();

    }
}
