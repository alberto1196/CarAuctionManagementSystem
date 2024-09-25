using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;

namespace CarAuctionManagementSystem.Factories
{
    public class CarFactory
    {
       
        public Vehicle GetVehicle(Dictionary<string, object> constructorParams)
        {
            if (constructorParams.ContainsKey("type"))
            {
                switch (constructorParams["type"].ToString().ToLower())
                {
                    case "hatchback":
                        return new Hatchback(constructorParams);
                    case "suv":
                        return new SUV(constructorParams);
                    case "truck":
                        return new Truck(constructorParams);
                    case "sedan":
                        return new Sedan(constructorParams);
                    default:
                        throw new ArgumentException("Type mentioned does not exist");
                }
            }
             throw new ArgumentException("Type is required");

        }
        public static CarFactory GetInstance()
        {
            return new CarFactory();
        }
    }
}
