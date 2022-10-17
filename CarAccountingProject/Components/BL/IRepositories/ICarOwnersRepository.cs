using System;
using System.Collections.Generic;

namespace BL
{
    public interface ICarOwnersRepository
    {
        List<CarOwner> GetCarOwners(int offset = 0, int limit = -1);
        List<CarOwner> GetCarOwnersByName(string name);
        List<CarOwner> GetCarOwnersBySurname(string surname);
        CarOwner GetCarOwnerByEmail(string email);
        CarOwner GetCarOwnerById(int id);
        void AddCarOwner(CarOwner owner);
        void UpdateCarOwner(int id, CarOwner newOwner);
        void DeleteCarOwner(int id);
    }
}