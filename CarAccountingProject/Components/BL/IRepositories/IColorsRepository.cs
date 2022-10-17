using System;
using System.Collections.Generic;

namespace BL
{
    public interface IColorsRepository
    {
        List<Color> GetColors(int offset = 0, int limit = -1);
        Color GetColorByName(string name);
        Color GetColorById(int id);
        void AddColor(Color col);
        void UpdateColor(int id, Color newColor);
        void DeleteColor(int id);
    }
}