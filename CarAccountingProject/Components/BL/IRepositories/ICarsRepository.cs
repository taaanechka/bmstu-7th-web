using System;
using System.Collections.Generic;

namespace BL
{
    public interface ICarsRepository
    {
        List<Car> GetCars(int offset = 0, int limit = -1);
        List<Car> GetCarsByModelId(int mId);
        List<Car> GetCarsByEquipmentId(int eId);
        List<Car> GetCarsByColorId(int colId);
        Car GetCarByComingId(int comingId);
        Car GetCarById(string id);
        int GetComingIdByCarId(string id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(string id, Car newCar);
        Task DeleteCarAsync(string id);
    }
}