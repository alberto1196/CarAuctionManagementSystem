namespace CarAuctionManagementSystem.Utils
{
    public class IDGenerator
    {
        private static int currentAuctionId;
        

        private IDGenerator()
        {
            currentAuctionId = 0;
            
        }
        
        public static int GetNextAuctionId()
        {
            return Interlocked.Increment(ref currentAuctionId);
        }
    }
}
