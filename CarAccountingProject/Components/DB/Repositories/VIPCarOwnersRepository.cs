using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class VIPCarOwnersRepository: IVIPCarOwnersRepository
    {
        private ApplicationContext db;

        public VIPCarOwnersRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.VIPCarOwner> GetVIPCarOwners(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var VIPcarOwners = db.VIPCarOwners.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.VIPCarOwners.Count())
            {
                VIPcarOwners = VIPcarOwners.Take(limit);
            }
            
            var VIPcarOwnersDB = VIPcarOwners.AsNoTracking().ToList();

            List<BL.VIPCarOwner> res = new List<BL.VIPCarOwner>();

            foreach (var elem in VIPcarOwnersDB)
            {
                res.Add(VIPCarOwnerConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.VIPCarOwner GetVIPCarOwnerById(int id)
        {
            try
            {
                return VIPCarOwnerConverter.DBToBL(db.VIPCarOwners.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new CarOwnerNotFoundException("GetVIPCarOwnerById() Error", exc.InnerException);
            }  
        }
    }
}