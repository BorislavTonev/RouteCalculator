using Router.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouteCalculator.Data.Interfaces
{
    public interface ILocationsRepository
    {
        List<Location> GetAllLocations();
        bool AddLocation(Location newRoad);
        bool EditLocation(int id);
    }
}
