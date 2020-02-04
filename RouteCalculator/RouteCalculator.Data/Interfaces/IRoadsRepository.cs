using Router.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouteCalculator.Data.Interfaces
{
    public interface IRoadsRepository
    {
        List<Road> GetAllRoads();
        bool AddRoad(Road newRoad);
        bool EditRoad(Road newRoad);

    }
}
