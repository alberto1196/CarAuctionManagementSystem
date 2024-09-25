using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Utils;

namespace CarAuctionManagementSystem.Services
{
    public interface IVehicleService
    {
        OperationResult AddVehicle(Dictionary<string,object>constructorParams);
        OperationResult<IEnumerable<Vehicle>> GetByType(string type);

        OperationResult<IEnumerable<Vehicle>> GetByModel(string model);

        OperationResult<IEnumerable<Vehicle>> GetByManufacturer(string manufacturer);

        OperationResult<IEnumerable<Vehicle>> GetByYear(int year);

        OperationResult<IEnumerable<Vehicle>> GetAll();
    }
}
