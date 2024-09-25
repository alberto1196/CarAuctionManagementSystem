using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;
using CarAuctionManagementSystem.Repository;

namespace CarAuctionManagementSystem.Inventory
{
    public class VehicleRepository:IVehicleRepository
    {
        public Dictionary<int/*vehicleID*/,Vehicle> Vehicles;

        public VehicleRepository() 
        { 
            Vehicles= new Dictionary<int, Vehicle>();
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return Vehicles.Values;
        }

        public Vehicle GetById(int id)
        {
            return Vehicles.ContainsKey(id) ? Vehicles[id]: null;
        }

        public IEnumerable<Vehicle> GetByManufacturer(string manufacturer)
        {
            return Vehicles.Values.Where(v=>v.Manufacturer==manufacturer);
        }

        public IEnumerable<Vehicle> GetByModel(string model)
        {
            return Vehicles.Values.Where(v => v.Model == model);
        }

        public IEnumerable<Vehicle> GetByType(string type)
        {
            return Vehicles.Values.Where(v => v.Type== type);
        }

        public IEnumerable<Vehicle> GetByYear(int year)
        {
            return Vehicles.Values.Where(v => v.Year == year);
        }

        public void Insert(Vehicle vehicle)
        {
            Vehicles.Add(vehicle.Id, vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            if(Vehicles.ContainsKey(vehicle.Id))
                Vehicles[vehicle.Id] = vehicle;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
