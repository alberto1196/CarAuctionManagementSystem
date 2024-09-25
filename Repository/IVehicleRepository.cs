using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Models.Vehicles;

namespace CarAuctionManagementSystem.Repository
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetAll();

        IEnumerable<Vehicle> GetByType(string type);

        IEnumerable<Vehicle> GetByManufacturer(string manufacturer);

        IEnumerable<Vehicle> GetByYear(int Year);

        IEnumerable<Vehicle> GetByModel(string model);

        Vehicle? GetById(int id);

        void Insert(Vehicle vehicle);


        void Update();

        
    }
}
