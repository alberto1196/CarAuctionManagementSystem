using System.Text.Json;

namespace CarAuctionManagementSystem.Models.Vehicles
{
    public class Truck : Vehicle
    {
        public int LoadCapacity { get; set; }

        public override string Type { get; set ; }

        public Truck(int id, string manufacturer, string modeL, int year, double startingBid, int loadCapacity) : base(id, manufacturer, modeL, year, startingBid)
        {
            LoadCapacity = loadCapacity;
            Type = "Truck";
        }
        public Truck(Dictionary<string, object> constructorParams) : base(constructorParams)
        {
            LoadCapacity = constructorParams.ContainsKey("loadCapacity") ? Convert.ToInt32(constructorParams["loadCapacity"].ToString()) : throw new ArgumentException("Load capacity is required");
            Type = "Truck";
        }

        public override string GetVehicleInfo()
        {
            return JsonSerializer.Serialize(this);
        }

        public override string ToString()
        {
            return base.ToString()+ $" Load Capacity:{LoadCapacity}";
        }
    }
}
