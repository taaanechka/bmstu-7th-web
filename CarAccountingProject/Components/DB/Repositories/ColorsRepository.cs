using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class ColorsRepository: IColorsRepository
    {
        private ApplicationContext db;

        public ColorsRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Color> GetColors(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var colors = db.Colors.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Colors.Count())
            {
                colors = colors.Take(limit);
            }
            
            var colorsDB = colors.AsNoTracking().ToList();

            List<BL.Color> res = new List<BL.Color>();

            foreach (var elem in colorsDB)
            {
                res.Add(ColorConverter.DBToBL(elem));
            }

            return res;
        }

        public BL.Color GetColorByName(string name)
        {
            try
            {
                Color colorDB = (from p in db.Colors.AsNoTracking()
                        where p.Name == name
                        select p).First();

                return ColorConverter.DBToBL(colorDB); 
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new ColorNotFoundException("GetColorByName() Error", exc.InnerException);
            }    
        }

        public BL.Color GetColorById(int id)
        {
            try
            {
                return ColorConverter.DBToBL(db.Colors.Find(id));
            }
            catch (Exception exc) // ArgumentNullException
            {
                throw new ColorNotFoundException("GetColorById() Error", exc.InnerException);
            }  
        }

        public void AddColor(BL.Color color)
        {
            // Validation
            try
            {
                Color colorDB = ColorConverter.BLToDB(color);
                ColorsValidator.ValidateColor(colorDB);
            }
            catch (Exception)
            {
                throw new ColorsValidatorFailException();
            }

            // Exists
            Color colorDupl = (from p in db.Colors.AsNoTracking()
                    where (p.Name == color.Name)
                        || (p.Id == color.Id)
                    select p).FirstOrDefault();

            if (colorDupl != null)
            {
                throw new ColorExistsException();
            }

            db.Colors.Add(ColorConverter.BLToDB(color));
            db.SaveChanges();        
        }

        public void UpdateColor(int id, BL.Color newColor)
        {
            Color color = db.Colors.Find(id);

            // NotFound
            if (color == null)
            {
                throw new ColorNotFoundException();
            }

            // Validation
            try
            {
                Color colorDB = ColorConverter.BLToDB(newColor);
                ColorsValidator.ValidateColor(colorDB);
            }
            catch (Exception)
            {
                throw new ColorsValidatorFailException();
            }

            // Exists
            Color colorDupl = (from p in db.Colors.AsNoTracking()
                    where (p.Name == newColor.Name)
                        && (p.Id != id)
                    select p).FirstOrDefault();

            if (colorDupl != null)
            {
                throw new ColorExistsException();
            }

            color.Name = newColor.Name;
            db.SaveChanges();
        }

        public void DeleteColor(int id)
        {
            Color color = db.Colors.Find(id);

            // NotFound
            if (color == null)
            {
                throw new ColorNotFoundException();
            }

            db.Colors.Remove(color);
            db.SaveChanges();
        }
    }
}