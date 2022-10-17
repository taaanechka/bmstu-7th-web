using System;
using System.Collections.Generic;

namespace BL
{
    public interface IDeparturesRepository
    {
        List<Departure> GetDepartures(int offset = 0, int limit = -1);
        List<Departure> GetDeparturesByDate(DateTime date);
        List<Departure> GetDeparturesBetweenDates(DateTime date1, DateTime date2);
        List<Departure> GetDeparturesByUserId(int uId);
        Departure GetDepartureById(int id);
        Task AddDepartureAsync(Departure departure, LinkOwnerCarDeparture link);
        Task DeleteDepartureAsync(int id);
    }
}