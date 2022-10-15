using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class MySQLDeparturesRepository: IDeparturesRepository
    {
        private MySQLApplicationContext db;

        public MySQLDeparturesRepository(MySQLApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Departure> GetDepartures(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var departures = db.Departures.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Departures.Count())
            {
                departures = departures.Take(limit);
            }
            
            var departuresDB = departures.AsNoTracking().ToList();

            List<BL.Departure> res = new List<BL.Departure>();

            foreach (var elem in departuresDB)
            {
                res.Add(DepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Departure> GetDeparturesByDate(DateTime date)
        {
            List<Departure> DeparturesDB = (from p in db.Departures.AsNoTracking()
                            where p.DepartureDate == date
                            select p).ToList();

            List<BL.Departure> res = new List<BL.Departure>();

            foreach (var elem in DeparturesDB)
            {
                res.Add(DepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Departure> GetDeparturesBetweenDates(DateTime date1, DateTime date2)
        {
            List<Departure> DeparturesDB = (from p in db.Departures.AsNoTracking()
                            where p.DepartureDate >= date1 && p.DepartureDate <= date2
                            select p).ToList();

            List<BL.Departure> res = new List<BL.Departure>();

            foreach (var elem in DeparturesDB)
            {
                res.Add(DepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Departure> GetDeparturesByUserId(int uId)
        {
            List<Departure> departuresDB = (from p in db.Departures.AsNoTracking()
                            where p.UserId == uId
                            select p).ToList();

            List<BL.Departure> res = new List<BL.Departure>();

            foreach (var elem in departuresDB)
            {
                res.Add(DepartureConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Departure GetDepartureById(int id)
        {
            try
            {
                return DepartureConverter.DBToBL(db.Departures.Find(id));
            }
            catch (Exception) // ArgumentNullException
            {
                throw new DepartureNotFoundException();
            }
        }

        public async Task AddDepartureAsync(BL.Departure departure, BL.LinkOwnerCarDeparture link)
        {
            // Validation
            try
            {
                Departure DepartureDB = DepartureConverter.BLToDB(departure);
                DeparturesValidator.ValidateDeparture(DepartureDB);
            }
            catch (Exception)
            {
                throw new DeparturesValidatorFailException();
            }

            // Exists departure
            Departure departureDupl = db.Departures.Find(departure.Id);

            if (departureDupl != null)
            {
                throw new DepartureExistsException();
            }

            // Exists link
            LinkOwnerCarDeparture LinkOwnerCarDepartureDupl = (from p in db.LinksOwnerCarDeparture.AsNoTracking()
                    where p.CarId == link.CarId ||
                            p.Id == link.Id
                    select p).FirstOrDefault();

            if (LinkOwnerCarDepartureDupl != null)
            {
                throw new LinkOwnerCarDepartureExistsException();
            }

            Departure departureDB = DepartureConverter.BLToDB(departure);

            db.Departures.Add(DepartureConverter.BLToDB(departure));
            db.SaveChanges(); 

            BL.LinkOwnerCarDeparture newLink = new BL.LinkOwnerCarDeparture(link.Id, link.OwnerId, link.CarId, departureDB.Id);
            MySQLLinksOwnerCarDepartureRepository linkRep = new MySQLLinksOwnerCarDepartureRepository(db);
            await linkRep.AddLinkOwnerCarDepartureAsync(newLink);    
        }

        public async Task DeleteDepartureAsync(int id)
        {
            Departure Departure = db.Departures.Find(id);

            // NotFound
            if (Departure == null)
            {
                throw new DepartureNotFoundException();
            }

            db.Departures.Remove(Departure);
            db.SaveChanges();
        }
    }
}