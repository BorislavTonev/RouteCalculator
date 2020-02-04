using RouteCalculator.Data.Interfaces;
using Router.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteCalculator.Data.Repository
{
    public class LocationsRepository : ILocationsRepository       
    {
        private readonly RouterContext _context;

        public LocationsRepository(RouterContext context)
        {
            this._context = context;
        }


        public List<Location> GetAllLocations()
        {
            var locations = _context.Locations.ToList();
            var result = new List<Location>();
            if (locations.Count != 0)
            {
                foreach (var item in locations)
                {
                    result.Add(new Location
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }

            }
           
            return result;
        }

        public bool AddLocation(Location loc)
        {
            return true;
        }

        public bool EditLocation(int id)
        {
            return true;
        }
    }
}
