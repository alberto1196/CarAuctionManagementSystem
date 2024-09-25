using CarAuctionManagementSystem.Factories;
using CarAuctionManagementSystem.Models;
using CarAuctionManagementSystem.Repository;
using CarAuctionManagementSystem.Utils;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public OperationResult AddVehicle(Dictionary<string, object> constructorParams)
        {

            try
            {
                var newVehicle = CarFactory.GetInstance().GetVehicle(constructorParams);

                if (_vehicleRepository.GetById(newVehicle.Id) != null)
                {
                    return new OperationResult { Success = false, Message = $"Error while inserting, a Vehicle with the ID {newVehicle.Id} already exist" };
                }

                _vehicleRepository.Insert(newVehicle);

                return new OperationResult { Success = true, Message = $"The Vehicle with the ID {newVehicle.Id} was successfuly inserted" };

            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }

        public OperationResult<IEnumerable<Vehicle>> GetByModel(string model)
        {
            try
            {
                var result = _vehicleRepository.GetByModel(model);
                return new OperationResult<IEnumerable<Vehicle>> { Data = result, Success = true, Message = $"Number of Result ({result.Count()})" };

            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<Vehicle>> { Data = null, Success = false, Message = ex.Message };
            }

        }

        public OperationResult<IEnumerable<Vehicle>> GetByType(string type)
        {
            try
            {
                var result = _vehicleRepository.GetByType(type);
                return new OperationResult<IEnumerable<Vehicle>> { Data = result, Success = true, Message = $"Number of Result ({result.Count()})" };

            }
            catch (Exception ex)
            {

                return new OperationResult<IEnumerable<Vehicle>> { Data = null, Success = false, Message = ex.Message };

            }
        }

        public OperationResult<IEnumerable<Vehicle>> GetByYear(int year)
        {
            try
            {
                var result = _vehicleRepository.GetByYear(year);
                return new OperationResult<IEnumerable<Vehicle>> { Data = result, Success = true, Message = $"Number of Result ({result.Count()})" };

            }
            catch (Exception ex)
            {

                return new OperationResult<IEnumerable<Vehicle>> { Data = null, Success = false, Message = ex.Message };

            }
        }

        public OperationResult<IEnumerable<Vehicle>> GetByManufacturer(string manufacturer)
        {
            try
            {
                var result = _vehicleRepository.GetByManufacturer(manufacturer);
                return new OperationResult<IEnumerable<Vehicle>> { Data = result, Success = true, Message = $"Number of Result ({result.Count()})" };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<Vehicle>> { Data = null, Success = false, Message = ex.Message };
            }
        }

        public OperationResult<IEnumerable<Vehicle>> GetAll()
        {
            try
            {
                var result = _vehicleRepository.GetAll();
                return new OperationResult<IEnumerable<Vehicle>> { Data = result, Success = true, Message = $"Number of Result ({result.Count()})" };

            }
            catch (Exception ex)
            {

                return new OperationResult<IEnumerable<Vehicle>> { Data = null, Success = false, Message = ex.Message };

            }
        }
    }
}
