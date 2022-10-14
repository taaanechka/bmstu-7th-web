using System;
using System.Collections.Generic;

namespace BL
{
    public interface IEquipmentsRepository
    {
        List<Equipment> GetEquipments(int offset = 0, int limit = -1);
        List<Equipment> GetEquipmentsByCategory(string category);
        List<Equipment> GetEquipmentsByGear(string gear);
        List<Equipment> GetEquipmentsByRoofType(string rooftype);
        Equipment GetEquipmentById(int id);
        void AddEquipment(Equipment eq);
        void UpdateEquipment(int id, Equipment newEquipment);
        void DeleteEquipment(int id);
    }
}