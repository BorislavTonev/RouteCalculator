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
            var newRec = new Location()
            {
                Name = loc.Name,
                CalculatonDate = DateTime.Now
            };

            _context.Locations.Add(newRec);

            return _context.SaveChanges() > 0;
        }

        public bool EditLocation(Location loc)
        {
            var record = _context.Locations.Where(r => r.Id == loc.Id).FirstOrDefault();
            if (record != null)
            {
                record.Name = loc.Name;
                record.CalculatonDate = loc.CalculatonDate;
                return _context.SaveChanges() > 0;
            }

            return false;
        }
    }
}
