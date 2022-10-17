using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class CarOwnersRepository: ICarOwnersRepository
    {
        private ApplicationContext db;

        public CarOwnersRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.CarOwner> GetCarOwners(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var carOwners = db.CarOwners.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.CarOwners.Count())
            {
                carOwners = carOwners.Take(limit);
            }
            
            var carOwnersDB = carOwners.AsNoTracking().ToList();

            List<BL.CarOwner> res = new List<BL.CarOwner>();

            foreach (var elem in carOwnersDB)
            {
                res.Add(CarOwnerConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.CarOwner> GetCarOwnersByName(string name)
        {
            List<CarOwner> ownersDB = (from p in db.CarOwners.AsNoTracking()
                            where p.Name == name
                            select p).ToList();

            List<BL.CarOwner> res = new List<BL.CarOwner>();

            foreach (var elem in ownersDB)
            {
                res.Add(CarOwnerConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.CarOwner> GetCarOwnersBySurname(string surname)
        {
            List<CarOwner> ownersDB = (from p in db.CarOwners.AsNoTracking()
                            where p.Surname == surname
                            select p).ToList();

            List<BL.CarOwner> res = new List<BL.CarOwner>();

            foreach (var elem in ownersDB)
            {
                res.Add(CarOwnerConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.CarOwner GetCarOwnerByEmail(string email)
        {
            try
            {
                CarOwner ownerDB = (from p in db.CarOwners.AsNoTracking()
                        where p.Email == email
                        select p).First();

                return CarOwnerConverter.DBToBL(ownerDB);
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new CarOwnerNotFoundException("GetCarOwnerByEmail() Error", exc.InnerException);
            }    
        }
        public BL.CarOwner GetCarOwnerById(int id)
        {
            try
            {
                return CarOwnerConverter.DBToBL(db.CarOwners.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new CarOwnerNotFoundException("GetCarOwnerById() Error", exc.InnerException);
            }  
        }

        public void AddCarOwner(BL.CarOwner owner)
        {
            // Validation
            try
            {
                DB.CarOwner ownerDB = CarOwnerConverter.BLToDB(owner);
                CarOwnersValidator.ValidateCarOwner(ownerDB);
            }
            catch (Exception)
            {
                throw new CarOwnersValidatorFailException();
            }

            // Exists
            CarOwner ownerDupl = (from p in db.CarOwners.AsNoTracking()
                    where (p.Email == owner.Email)
                        || (p.Id == owner.Id)
                    select p).FirstOrDefault();

            if (ownerDupl != null)
            {
                throw new CarOwnerExistsException();
            }

            db.CarOwners.Add(CarOwnerConverter.BLToDB(owner));
            db.SaveChanges();
        }
        
        public void UpdateCarOwner(int id, BL.CarOwner newOwner)
        {
            CarOwner owner = db.CarOwners.Find(id);

            // NotFound
            if (owner == null)
            {
                throw new CarOwnerNotFoundException();
            }

            // Validation
            try
            {
                CarOwner ownerDB = CarOwnerConverter.BLToDB(newOwner);
                CarOwnersValidator.ValidateCarOwner(ownerDB);
            }
            catch (Exception)
            {
                throw new CarOwnersValidatorFailException();
            }

            // Exists
            CarOwner ownerDupl = (from p in db.CarOwners.AsNoTracking()
                    where (p.Email == newOwner.Email)
                        && (p.Id != id)
                    select p).FirstOrDefault();

            if (ownerDupl != null)
            {
                throw new CarOwnerExistsException();
            }

            owner.Name = newOwner.Name;
            owner.Surname = newOwner.Surname;
            owner.Email = newOwner.Email;
            db.SaveChanges();
        }

        public void DeleteCarOwner(int id)
        {
            CarOwner owner = db.CarOwners.Find(id);

            // NotFound
            if (owner == null)
            {
                throw new CarOwnerNotFoundException();
            }

            db.CarOwners.Remove(owner);
            db.SaveChanges();
        }
    }
}