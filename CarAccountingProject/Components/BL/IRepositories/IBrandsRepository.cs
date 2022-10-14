using System;
using System.Collections.Generic;

namespace BL
{
    public interface IBrandsRepository
    {
        List<Brand> GetBrands(int offset = 0, int limit = -1);
        List<Brand> GetBrandsByManufactCountry(string mc);
        List<Brand> GetBrandsByWheel(string wh);
        Brand GetBrandByName(string name);
        Brand GetBrandById(int id);
        void AddBrand(Brand brand);
        void UpdateBrand(int id, Brand newBrand); 
        void DeleteBrand(int id);
    }
}