using System;
using System.Collections.Generic;

namespace BL
{
    public interface ILinksOwnerCarDepartureRepository
    {
        List<LinkOwnerCarDeparture> GetLinksOwnerCarDeparture(int offset = 0, int limit = -1);
        List<LinkOwnerCarDeparture> GetLinksOwnerCarDepartureByOwnerId(int oId);
        LinkOwnerCarDeparture GetLinkOwnerCarDepartureByCarId(string carId);
        LinkOwnerCarDeparture GetLinkOwnerCarDepartureByDepartureId(int depId);
        LinkOwnerCarDeparture GetLinkOwnerCarDepartureById(int id);
        List<Car> GetCarsNotInLinksOwnerCarDeparture();
        Car GetCarIfNotInLinksOwnerCarDeparture(string id);
        string GetCarIdByDepartureId(int depId);
        int GetDepartureIdByCarId(string carId);
        Task AddLinkOwnerCarDepartureAsync(LinkOwnerCarDeparture ocd);
        Task DeleteLinkOwnerCarDepartureAsync(int id);
    }
}