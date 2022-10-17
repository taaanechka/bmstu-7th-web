using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class EquipmentsRepository: IEquipmentsRepository
    {
        private ApplicationContext db;

        public EquipmentsRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Equipment> GetEquipments(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var equipments = db.Equipments.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Equipments.Count())
            {
                equipments = equipments.Take(limit);
            }
            
            var equipmentsDB = equipments.AsNoTracking().ToList();

            List<BL.Equipment> res = new List<BL.Equipment>();

            foreach (var elem in equipmentsDB)
            {
                res.Add(EquipmentConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Equipment> GetEquipmentsByCategory(string category)
        {
            List<Equipment> eqsDB = (from p in db.Equipments.AsNoTracking()
                            where p.Category == category
                            select p).ToList();

            List<BL.Equipment> res = new List<BL.Equipment>();

            foreach (var elem in eqsDB)
            {
                res.Add(EquipmentConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Equipment> GetEquipmentsByGear(string gear)
        {
            List<Equipment> eqsDB = (from p in db.Equipments.AsNoTracking()
                            where p.Gear == gear
                            select p).ToList();

            List<BL.Equipment> res = new List<BL.Equipment>();

            foreach (var elem in eqsDB)
            {
                res.Add(EquipmentConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Equipment> GetEquipmentsByRoofType(string rooftype)
        {
            List<Equipment> eqsDB = (from p in db.Equipments.AsNoTracking()
                            where p.RoofType == rooftype
                            select p).ToList();

            List<BL.Equipment> res = new List<BL.Equipment>();

            foreach (var elem in eqsDB)
            {
                res.Add(EquipmentConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Equipment GetEquipmentById(int id)
        {
            try
            {
                return EquipmentConverter.DBToBL(db.Equipments.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new EquipmentNotFoundException("GetEquipmentById() Error", exc.InnerException);
            }
        }

        public void AddEquipment(BL.Equipment equipment)
        {
            // Validation
            try
            {
                Equipment equipmentDB = EquipmentConverter.BLToDB(equipment);
                EquipmentsValidator.ValidateEquipment(equipmentDB);
            }
            catch (Exception)
            {
                throw new EquipmentsValidatorFailException();
            }

            // Exists
            Equipment EquipmentDupl = (from p in db.Equipments.AsNoTracking()
                    where (p.Id == equipment.Id) ||
                        ((p.Category == equipment.Category)
                        && (p.Gear == equipment.Gear)
                        && (p.RoofType == equipment.RoofType))
                    select p).FirstOrDefault();

            if (EquipmentDupl != null)
            {
                throw new EquipmentExistsException();
            }

            db.Equipments.Add(EquipmentConverter.BLToDB(equipment));
            db.SaveChanges();
        }
        
        public void UpdateEquipment(int id, BL.Equipment newEquipment)
        {
            Equipment equipment = db.Equipments.Find(id);

            // NotFound
            if (equipment == null)
            {
                throw new EquipmentNotFoundException();
            }

            // Validation
            try
            {
                Equipment equipmentDB = EquipmentConverter.BLToDB(newEquipment);
                EquipmentsValidator.ValidateEquipment(equipmentDB);
            }
            catch (Exception)
            {
                throw new EquipmentsValidatorFailException();
            }

            // Exists
            Equipment EquipmentDupl = (from p in db.Equipments.AsNoTracking()
                    where (p.Category == newEquipment.Category)
                        && (p.Gear == newEquipment.Gear)
                        && (p.RoofType == newEquipment.RoofType)
                        && (p.Id != id)
                    select p).FirstOrDefault();

            if (EquipmentDupl != null)
            {
                throw new EquipmentExistsException();
            }

            equipment.Category = newEquipment.Category;
            equipment.Gear = newEquipment.Gear;
            equipment.RoofType = newEquipment.RoofType;
            db.SaveChanges();
        }

        public void DeleteEquipment(int id)
        {
            Equipment equipment = db.Equipments.Find(id);

            // NotFound
            if (equipment == null)
            {
                throw new EquipmentNotFoundException();
            }

            db.Equipments.Remove(equipment);
            db.SaveChanges();
        }
    }
}