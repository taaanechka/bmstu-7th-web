using System;
using System.Collections.Generic;

namespace BL
{
    public interface IModelsRepository
    {
        List<Model> GetModels(int offset = 0, int limit = -1);
        List<Model> GetModelsByName(string name);
        List<Model> GetModelsByBrandId(int bId);
        Model GetModelById(int id);
        void AddModel(Model m);
        void UpdateModel(int id, Model newModel);
        void DeleteModel(int id);
    }
}