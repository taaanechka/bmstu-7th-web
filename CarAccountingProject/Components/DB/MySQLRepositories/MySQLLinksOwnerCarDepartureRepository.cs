using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class MySQLLinksOwnerCarDepartureRepository: ILinksOwnerCarDepartureRepository
    {
        private MySQLApplicationContext db;

        public MySQLLinksOwnerCarDepartureRepository(MySQLApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.LinkOwnerCarDeparture> GetLinksOwnerCarDeparture(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var LinksOwnerCarDeparture = db.LinksOwnerCarDeparture.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.LinksOwnerCarDeparture.Count())
            {
                LinksOwnerCarDeparture = LinksOwnerCarDeparture.Take(limit);
            }
            
            var LinksOwnerCarDepartureDB = LinksOwnerCarDeparture.AsNoTracking().ToList();

            List<BL.LinkOwnerCarDeparture> res = new List<BL.LinkOwnerCarDeparture>();

            foreach (var elem in LinksOwnerCarDepartureDB)
            {
                res.Add(LinkOwnerCarDepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.LinkOwnerCarDeparture> GetLinksOwnerCarDepartureByOwnerId(int oId)
        {
            List<LinkOwnerCarDeparture> LinksOwnerCarDepartureDB = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                            where p.OwnerId == oId
                            select p).ToList();

            List<BL.LinkOwnerCarDeparture> res = new List<BL.LinkOwnerCarDeparture>();

            foreach (var elem in LinksOwnerCarDepartureDB)
            {
                res.Add(LinkOwnerCarDepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureByCarId(string cId)
        {
            try
            {
                LinkOwnerCarDeparture LinkOwnerCarDepartureDB = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                            where p.CarId == cId
                            select p).First();

                return LinkOwnerCarDepartureConverter.DBToBL(LinkOwnerCarDepartureDB);
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new LinkOwnerCarDepartureNotFoundException("GetLinkOwnerCarDepartureByCarId() Error", exc.InnerException);
            }
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureByDepartureId(int depId)
        {
            try
            {
                LinkOwnerCarDeparture LinkOwnerCarDepartureDB = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                            where p.DepartureId == depId
                            select p).First();

                return LinkOwnerCarDepartureConverter.DBToBL(LinkOwnerCarDepartureDB);
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new LinkOwnerCarDepartureNotFoundException("GetLinkOwnerCarDepartureByCarId() Error", exc.InnerException);
            }
        }

        public BL.LinkOwnerCarDeparture GetLinkOwnerCarDepartureById(int id)
        {
            try
            {
                return LinkOwnerCarDepartureConverter.DBToBL(db.LinksOwnerCarDeparture.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new LinkOwnerCarDepartureNotFoundException("GetLinkOwnerCarDepartureById() Error", exc.InnerException);
            }
        }

        public List<BL.Car> GetCarsNotInLinksOwnerCarDeparture()
        {
            var carsRep = new MySQLCarsRepository(db);

            List<BL.Car> carsBLAll = carsRep.GetCars();
            if (carsBLAll == null)
            {
                return null;
            }

            var carsDBAll = new List<Car>();

            foreach (var elem in carsBLAll)
            {
                carsDBAll.Add(CarConverter.BLToDB(elem));
            }

            List<Car> CarsDB = (from p in db.LinksOwnerCarDeparture
                            join car in db.Cars on p.CarId equals car.Id
                            select new Car
                            {
                                Id = car.Id,
                                ModelId = car.ModelId,
                                EquipmentId = car.EquipmentId,
                                ColorId = car.ColorId,
                                ComingId = car.ComingId
                            }
                            ).ToList();

            var resDB = (from p in CarsDB
                        where !carsDBAll.Contains(p)
                        select p).ToList();

            List<BL.Car> res = new List<BL.Car>();

            foreach (var elem in resDB)
            {
                res.Add(CarConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Car GetCarIfNotInLinksOwnerCarDeparture(string id)
        {
            var carsRep = new MySQLCarsRepository(db);

            List<BL.Car> carsBLAll = carsRep.GetCars();
            if (carsBLAll == null)
            {
                return null;
            }

            var carsDBAll = new List<Car>();

            foreach (var elem in carsBLAll)
            {
                carsDBAll.Add(CarConverter.BLToDB(elem));
            }

            List<Car> CarsDB = (from p in db.LinksOwnerCarDeparture
                            join car in db.Cars on p.CarId equals car.Id
                            select new Car
                            {
                                Id = car.Id,
                                ModelId = car.ModelId,
                                EquipmentId = car.EquipmentId,
                                ColorId = car.ColorId,
                                ComingId = car.ComingId
                            }
                            ).ToList();

            var resDB = (from p in CarsDB
                        where !carsDBAll.Contains(p)
                            && p.Id ==id
                        select p).FirstOrDefault();

            return CarConverter.DBToBL(resDB);
        }

        public string GetCarIdByDepartureId(int depId)
        {
            try
            {
                string carId = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                            where p.DepartureId == depId
                            select p.CarId).First();

                return carId;
            }
            catch (Exception exc)
            {
                throw new LinkOwnerCarDepartureNotFoundException("GetCarIdByDepartureId() Error", exc.InnerException);
            }
        }

        public int GetDepartureIdByCarId(string carId)
        {
            try
            {
                int departureId = (from p in db.LinksOwnerCarDeparture
                            where p.CarId == carId
                            select p.DepartureId).First();

                return departureId;
            }
            catch (Exception exc)
            {
                throw new LinkOwnerCarDepartureNotFoundException("GetLinkOwnerCarDepartureByCarId() Error", exc.InnerException);
            }
        }

        public void AddLinkOwnerCarDeparture(BL.LinkOwnerCarDeparture LinkOwnerCarDeparture)
        {
            // Validation
            try
            {
                LinkOwnerCarDeparture LinkOwnerCarDepartureDB = LinkOwnerCarDepartureConverter.BLToDB(LinkOwnerCarDeparture);
                LinksOwnerCarDepartureValidator.ValidateLinkOwnerCarDeparture(LinkOwnerCarDepartureDB);
            }
            catch (Exception)
            {
                throw new LinksOwnerCarDepartureValidatorFailException();
            }

            // Exists
            LinkOwnerCarDeparture LinkOwnerCarDepartureDupl = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                    where ((p.OwnerId == LinkOwnerCarDeparture.OwnerId)
                        && (p.CarId == LinkOwnerCarDeparture.CarId)
                        && (p.DepartureId == LinkOwnerCarDeparture.DepartureId))
                        || (p.Id == LinkOwnerCarDeparture.Id)
                    select p).FirstOrDefault();

            if (LinkOwnerCarDepartureDupl != null)
            {
                throw new LinkOwnerCarDepartureExistsException();
            }

            db.LinksOwnerCarDeparture.Add(LinkOwnerCarDepartureConverter.BLToDB(LinkOwnerCarDeparture));
            db.SaveChanges();
        }

        public void DeleteLinkOwnerCarDeparture(int id)
        {
            LinkOwnerCarDeparture LinkOwnerCarDeparture = db.LinksOwnerCarDeparture.Find(id);

            // NotFound
            if (LinkOwnerCarDeparture == null)
            {
                throw new LinkOwnerCarDepartureNotFoundException();
            }

            db.LinksOwnerCarDeparture.Remove(LinkOwnerCarDeparture);
            db.SaveChanges();
        }
    }
}