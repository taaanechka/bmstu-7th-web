using System;
using System.Collections.Generic;

namespace BL
{
    public interface IComingsRepository
    {
        List<Coming> GetComings(int offset = 0, int limit = -1);
        List<Coming> GetComingsByDate(DateTime date);
        List<Coming> GetComingsBetweenDates(DateTime date1, DateTime date2);
        List<Coming> GetComingsByUserId(int uId);
        Coming GetComingById(int id);
        void AddComing(Coming com, Car car);
        void DeleteComing(int id);
    }
}