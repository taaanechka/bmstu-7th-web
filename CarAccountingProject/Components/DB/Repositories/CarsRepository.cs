using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class CarsRepository: ICarsRepository
    {
        private ApplicationContext db;

        public CarsRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Car> GetCars(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var cars = db.Cars.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Cars.Count())
            {
                cars = cars.Take(limit);
            }
            
            var carsDB = cars.AsNoTracking().ToList();

            List<BL.Car> res = new List<BL.Car>();

            foreach (var elem in carsDB)
            {
                res.Add(CarConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Car> GetCarsByModelId(int mId)
        {
            List<Car> carsDB = (from p in db.Cars.AsNoTracking()
                            where p.ModelId == mId
                            select p).ToList();

            List<BL.Car> res = new List<BL.Car>();

            foreach (var elem in carsDB)
            {
                res.Add(CarConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Car> GetCarsByEquipmentId(int eId)
        {
            List<Car> carsDB = (from p in db.Cars.AsNoTracking()
                            where p.EquipmentId == eId
                            select p).ToList();

            List<BL.Car> res = new List<BL.Car>();

            foreach (var elem in carsDB)
            {
                res.Add(CarConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Car> GetCarsByColorId(int colId)
        {
            List<Car> carsDB = (from p in db.Cars.AsNoTracking()
                            where p.ColorId == colId
                            select p).ToList();

            List<BL.Car> res = new List<BL.Car>();

            foreach (var elem in carsDB)
            {
                res.Add(CarConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Car GetCarByComingId(int comingId)
        {
            try
            {
                Car carDB = (from p in db.Cars.AsNoTracking()
                            where p.ComingId == comingId
                            select p).First();

                return CarConverter.DBToBL(carDB);
            }
            catch (Exception exc)
            {
                throw new CarNotFoundException("GetCarByComingId() Error", exc.InnerException);
            }
        }

        public BL.Car GetCarById(string id)
        {
            try
            {
                return CarConverter.DBToBL(db.Cars.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new CarNotFoundException("GetCarById() Error", exc.InnerException);
            }        
        }

        
        public int GetComingIdByCarId(string id)
        {
            try
            {
                int comingId = (from p in db.Cars.AsNoTracking()
                            where p.Id== id
                            select p.ComingId).First();

                return comingId;
            }
            catch (Exception exc)
            {
                throw new ComingNotFoundException("GetComingIdByCarId() Error", exc.InnerException);
            }
        }

        public async Task AddCarAsync(BL.Car car)
        {
            // Validation
            try
            {
                Car carDB = CarConverter.BLToDB(car);
                CarsValidator.ValidateCar(carDB);
            }
            catch (Exception)
            {
                throw new CarsValidatorFailException();
            }

            // Exists
            Car carDupl = (from p in db.Cars.AsNoTracking()
                    where p.Id == car.Id 
                    select p).FirstOrDefault();
            // Car? carDupl = (from p in db.Cars.AsNoTracking()
            //         where p.Id == car.Id ||
            //             p.ComingId == car.ComingId
            //         select p).FirstOrDefault();

            if (carDupl != null)
            {
                throw new CarExistsException();
            }
            
            db.Cars.Add(CarConverter.BLToDB(car));
            await db.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(string id, BL.Car newCar)
        {
            Car car = db.Cars.Find(id);

            // NotFound
            if (car == null)
            {
                throw new CarNotFoundException();
            }

            // Validation
            try
            {
                Car carDB = CarConverter.BLToDB(newCar);
                CarsValidator.ValidateCar(carDB);
            }
            catch (Exception)
            {
                throw new CarsValidatorFailException();
            }

            car.EquipmentId = newCar.EquipmentId;
            car.ColorId = newCar.ColorId;
            await db.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(string id)
        {
            Car car = db.Cars.Find(id);

            // NotFound
            if (car == null)
            {
                throw new CarNotFoundException();
            }

            db.Cars.Remove(car);
            await db.SaveChangesAsync();
        }
    }
}