using System.Text.Json;

namespace CarAuctionManagementSystem.Models.Vehicles
{
    public class SUV:Vehicle
    {
        int NumberOfSeat { get; set; }
        public override string Type { get; set; }
        public SUV(int id, string manufacturer, string modeL, int year, double startingBid, int numberOfSeat):base(id,manufacturer,modeL,year,startingBid)
        {
            NumberOfSeat = numberOfSeat;
            Type = "SUV";
        }
        public SUV(Dictionary<string, object> constructorParams) : base(constructorParams)
        {
            NumberOfSeat = constructorParams.ContainsKey("numberOfSeats") ? Convert.ToInt32(constructorParams["numberOfSeats"].ToString()) : throw new ArgumentException("Number of seats is required");
            Type = "SUV";
        }

        public override string GetVehicleInfo()
        {
            return JsonSerializer.Serialize(this);
        }
        public override string ToString()
        {
            return base.ToString() + $" Number of seats:{NumberOfSeat}";
        }
    }
}
