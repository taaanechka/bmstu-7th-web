using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class MySQLComingsRepository: IComingsRepository
    {
        private MySQLApplicationContext db;

        public MySQLComingsRepository(MySQLApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Coming> GetComings(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var comings = db.Comings.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Comings.Count())
            {
                comings = comings.Take(limit);
            }
            
            var comingsDB = comings.AsNoTracking().ToList();

            List<BL.Coming> res = new List<BL.Coming>();

            foreach (var elem in comingsDB)
            {
                res.Add(ComingConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Coming> GetComingsByDate(DateTime date)
        {
            List<Coming> comingsDB = (from p in db.Comings.AsNoTracking()
                            where p.ComingDate == date
                            select p).ToList();

            List<BL.Coming> res = new List<BL.Coming>();

            foreach (var elem in comingsDB)
            {
                res.Add(ComingConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Coming> GetComingsBetweenDates(DateTime date1, DateTime date2)
        {
            List<Coming> comingsDB = (from p in db.Comings.AsNoTracking()
                            where p.ComingDate >= date1 && p.ComingDate <= date2
                            select p).ToList();

            List<BL.Coming> res = new List<BL.Coming>();

            foreach (var elem in comingsDB)
            {
                res.Add(ComingConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Coming> GetComingsByUserId(int uId)
        {
            List<Coming> comingsDB = (from p in db.Comings.AsNoTracking()
                            where p.UserId == uId
                            select p).ToList();

            List<BL.Coming> res = new List<BL.Coming>();

            foreach (var elem in comingsDB)
            {
                res.Add(ComingConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Coming GetComingById(int id)
        {
            try
            {
                return ComingConverter.DBToBL(db.Comings.Find(id));
            }
            catch (Exception) // ArgumentNullException
            {
                throw new ComingNotFoundException();
            }
        }

        public void AddComing(BL.Coming coming, BL.Car car)
        {
            // Validation
            try
            {
                Coming comingTmp = ComingConverter.BLToDB(coming);
                ComingsValidator.ValidateComing(comingTmp);
            }
            catch (Exception)
            {
                throw new ComingsValidatorFailException();
            }

            // Exists coming
            Coming comingDupl = db.Comings.Find(coming.Id);

            if (comingDupl != null)
            {
                throw new ComingExistsException();
            }

            // Exists car
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

            Coming comingDB = ComingConverter.BLToDB(coming);

            db.Comings.Add(comingDB);
            db.SaveChanges();

            BL.Car newCar = new BL.Car(car.Id, car.ModelId, car.EquipmentId, car.ColorId, comingDB.Id);
            MySQLCarsRepository carRep = new MySQLCarsRepository(db);
            carRep.AddCar(newCar);
        }

        public void DeleteComing(int id)
        {
            Coming coming = db.Comings.Find(id);

            // NotFound
            if (coming == null)
            {
                throw new ComingNotFoundException();
            }

            db.Comings.Remove(coming);
            db.SaveChanges();
        }
    }
}