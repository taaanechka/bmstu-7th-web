using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class BrandsRepository: BL.IBrandsRepository
    {
        private ApplicationContext db;

        public BrandsRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Brand> GetBrands(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var brands = db.Brands.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Brands.Count())
            {
                brands = brands.Take(limit);
            }
            
            var brandsDB = brands.AsNoTracking().ToList();

            List<BL.Brand> res = new List<BL.Brand>();

            foreach (var elem in brandsDB)
            {
                res.Add(BrandConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Brand> GetBrandsByManufactCountry(string mc)
        {
            List<Brand> brandsDB = (from p in db.Brands.AsNoTracking()
                            where p.ManufactCountry == mc
                            select p).ToList();

            List<BL.Brand> res = new List<BL.Brand>();

            foreach (var elem in brandsDB)
            {
                res.Add(BrandConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Brand> GetBrandsByWheel(string wh)
        {
            List<Brand> brandsDB = (from p in db.Brands.AsNoTracking()
                            where p.Wheel == wh
                            select p).ToList();
                            
            List<BL.Brand> res = new List<BL.Brand>();

            foreach (var elem in brandsDB)
            {
                res.Add(BrandConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Brand GetBrandByName(string name)
        {
            try
            {
                Brand brandDB = (from p in db.Brands.AsNoTracking()
                            where p.Name == name
                            select p).First();

                return BrandConverter.DBToBL(brandDB);
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new BrandNotFoundException("GetBrandByName() Error", exc.InnerException);
            }
        }

        public BL.Brand GetBrandById(int id)
        {  
            try
            {
                var brandBL = BrandConverter.DBToBL(db.Brands.Find(id));
                return BrandConverter.DBToBL(db.Brands.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new BrandNotFoundException("GetBrandById() Error", exc.InnerException);
            }
        }

        public void AddBrand(BL.Brand brand)
        {
            // Validation
            try
            {
                DB.Brand brandDB = BrandConverter.BLToDB(brand);
                BrandsValidator.ValidateBrand(brandDB);
            }
            catch (Exception)
            {
                throw new BrandsValidatorFailException();
            }

            // Exists
            Brand brandDupl = (from p in db.Brands.AsNoTracking()
                    where (p.Name == brand.Name)
                        || (p.Id == brand.Id)
                    select p).FirstOrDefault();

            if (brandDupl != null)
            {
                throw new BrandExistsException();
            }

            db.Brands.Add(BrandConverter.BLToDB(brand));
            db.SaveChanges();
        }

        public void UpdateBrand(int id, BL.Brand newBrand)
        {
            Brand brand = db.Brands.Find(id);

            // NotFound
            if (brand == null)
            {
                throw new BrandNotFoundException();
            }

            // Validation
            try
            {
                Brand brandDB = BrandConverter.BLToDB(newBrand);
                BrandsValidator.ValidateBrand(brandDB);
            }
            catch (Exception)
            {
                throw new BrandsValidatorFailException();
            }

            // Exists
            Brand brandDupl = (from p in db.Brands.AsNoTracking()
                    where (p.Name == newBrand.Name)
                        && (p.Id != id)
                    select p).FirstOrDefault();

            if (brandDupl != null)
            {
                throw new BrandExistsException();
            }

            brand.Name = newBrand.Name;
            brand.ManufactCountry = newBrand.ManufactCountry;
            brand.Wheel = newBrand.Wheel;
            db.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            Brand brand = db.Brands.Find(id);

            // NotFound
            if (brand == null)
            {
                throw new BrandNotFoundException();
            }

            db.Brands.Remove(brand);
            db.SaveChanges();
        }
    }
}