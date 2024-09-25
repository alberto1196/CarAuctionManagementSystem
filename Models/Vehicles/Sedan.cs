using System.Text.Json;

namespace CarAuctionManagementSystem.Models.Vehicles
{
    public class Sedan : Vehicle
    {
        int NumberOfDors { get; set; }
        public override string Type { get; set; }
        public Sedan(int id, string manufacturer, string modeL, int year, double startingBid, int numberOfDors) : base(id, manufacturer, modeL, year, startingBid)
        {
            NumberOfDors = numberOfDors;
            Type = "Sedan";
        }
        public Sedan(Dictionary<string, object> constructorParams):base(constructorParams)
        {
            NumberOfDors = constructorParams.ContainsKey("numberOfDors") ? Convert.ToInt32(constructorParams["numberOfDors"].ToString()) : throw new ArgumentException("Number of dors is required");
            Type = "Sedan";
        }
        public override string GetVehicleInfo()
        {
            return JsonSerializer.Serialize(this);
        }
        public override string ToString()
        {
            return base.ToString() + $" Number of dors:{NumberOfDors}";
        }
    }
}
