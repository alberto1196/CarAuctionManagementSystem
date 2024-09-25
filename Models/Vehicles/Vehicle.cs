using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text.Json;
using System.Transactions;

namespace CarAuctionManagementSystem.Models
{
    public abstract class Vehicle
    {
        public int Id { get; init; }
        public  string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double StartingBid { get; set; }

        public abstract string Type { get; set; }

        protected Vehicle(int id, string manufacturer, string modeL, int year, double startingBid)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = modeL;
            Year = year;
            StartingBid = startingBid;
        }

        public Vehicle(Dictionary<string, object> constructorParams)
        {
            Id = constructorParams.ContainsKey("id") ? Convert.ToInt32( constructorParams["id"].ToString()) : throw new ArgumentException("Id Is required");
            Manufacturer = constructorParams.ContainsKey("manufacturer") ? constructorParams["manufacturer"].ToString() : throw new ArgumentException("manufacturer is required");
            Model = constructorParams.ContainsKey("model") ? constructorParams["model"].ToString() : throw new ArgumentException("model is required");
            Year = constructorParams.ContainsKey("year") ? Convert.ToInt32(constructorParams["year"].ToString()) : throw new ArgumentException("year Is required");
            StartingBid = ExtractDouble(constructorParams, "startingBid");
          
        }
      
        public double ExtractDouble(Dictionary<string,object> constructorParams,string key)
        {
            if (constructorParams.TryGetValue(key, out var startingBidObj))
            {
                if (startingBidObj is JsonElement element && element.ValueKind == JsonValueKind.Number)
                {
                    return element.GetDouble();

                }
                else
                {
                    return Convert.ToDouble( constructorParams[key]);
                }
            }
            else
            {
                throw new ArgumentException("year Is required");
            }
        }

        public abstract string GetVehicleInfo();

        public override bool Equals(object? obj)
        {
            if(obj==null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Vehicle other = (Vehicle)obj;
            
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Manufacturer: {Manufacturer}, Model: {Model}, Year:{Year}, StartingBid:{StartingBid},";
        }

    }
}
