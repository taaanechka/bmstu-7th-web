using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using BL;

#nullable disable

namespace DB
{
    public class MySQLModelsRepository : BL.IModelsRepository
    {
        private MySQLApplicationContext db;

        public MySQLModelsRepository(MySQLApplicationContext context)
        {
            this.db = context;
        }

        public List<BL.Model> GetModels(int offset = 0, int limit = -1)
        {
            if (offset < 0)
                offset = 0;

            var models = db.Models.OrderBy(p => p.Id).Skip(offset);

            if (limit > 0 && (offset + limit) <= db.Models.Count())
            {
                models = models.Take(limit);
            }
            
            var modelsDB = models.AsNoTracking().ToList();

            List<BL.Model> res = new List<BL.Model>();

            foreach (var elem in modelsDB)
            {
                res.Add(ModelConverter.DBToBL(elem));
            }

            return res;
        }

        public List<BL.Model> GetModelsByName(string name)
        {
            List<Model> modelsDB = (from p in db.Models.AsNoTracking()
                        where p.Name == name
                        select p).ToList();

            List<BL.Model> res = new List<BL.Model>();

            foreach (var elem in modelsDB)
            {
                res.Add(ModelConverter.DBToBL(elem));
            }

            return res;     
        }

        public List<BL.Model> GetModelsByBrandId(int bId)
        {
            List<Model> modelsDB = (from p in db.Models.AsNoTracking()
                        where p.BrandId == bId
                        select p).ToList();

            List<BL.Model> res = new List<BL.Model>();

            foreach (var elem in modelsDB)
            {
                res.Add(ModelConverter.DBToBL(elem));
            }

            return res;     
        }

        public BL.Model GetModelById(int id)
        {
            try
            {
                return ModelConverter.DBToBL(db.Models.Find(id));
            }
            catch (Exception) // ArgumentNullException
            {
                throw new ModelNotFoundException();
            }
        }

        public void AddModel(BL.Model model)
        {
            // Validation
            try
            {
                Model modelDB = ModelConverter.BLToDB(model);
                ModelsValidator.ValidateModel(modelDB);
            }
            catch (Exception)
            {
                throw new ModelsValidatorFailException();
            }

            // Exists
            Model modelDupl = (from p in db.Models.AsNoTracking()
                    where ((p.BrandId == model.BrandId)
                        && (p.Name == model.Name))
                        || (p.Id == model.Id)
                    select p).FirstOrDefault();

            if (modelDupl != null)
            {
                throw new ModelExistsException();
            }

            db.Models.Add(ModelConverter.BLToDB(model));
            db.SaveChanges();      
        }

        public void UpdateModel(int id, BL.Model newModel)
        {
            Model model = db.Models.Find(id);

            // NotFound
            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            // Validation
            try
            {
                Model modelDB = ModelConverter.BLToDB(newModel);
                ModelsValidator.ValidateModel(modelDB);
            }
            catch (Exception)
            {
                throw new ModelsValidatorFailException();
            }

            // Exists
            Model modelDupl = (from p in db.Models.AsNoTracking()
                    where (p.BrandId == newModel.BrandId)
                        && (p.Name == newModel.Name)
                        && (p.Id != id)
                    select p).FirstOrDefault();

            if (modelDupl != null)
            {
                throw new ModelExistsException();
            }

            model.Name = newModel.Name;
            db.SaveChanges();
        }

        public void DeleteModel(int id)
        {
            Model model = db.Models.Find(id);

            // NotFound
            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            db.Models.Remove(model);
            db.SaveChanges();
        }
    }
}