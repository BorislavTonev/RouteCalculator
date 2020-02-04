using RouteCalculator.Data.Interfaces;
using Router.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteCalculator.Data.Repository
{
    public class RoadsRepository: IRoadsRepository
    {
        private readonly RouterContext _context;

        public RoadsRepository(RouterContext context)
        {
            this._context = context;
        }


        public List<Road> GetAllRoads()
        {
            var locations = _context.Roads.ToList();
            var result = new List<Road>();
            if (locations.Count != 0)
            {
                foreach (var item in locations)
                {
                    result.Add(new Road
                    {
                        Id = item.Id,
                        Name = item.Name,
                        StartingPoint = item.StartingPoint,
                        EndingPoint = item.EndingPoint,
                        Distance = item.Distance
                    });
                }

            }

            return result;
        }

        public bool AddRoad(Road road)
        {
            var newRec = new Road() 
            {
                Name = road.Name,
                Distance = road.Distance,
                StartingPoint = road.StartingPoint,
                EndingPoint = road.EndingPoint
            };

            _context.Roads.Add(newRec);

            return _context.SaveChanges() > 0;
        }

        public bool EditRoad(Road road)
        {
            var record =_context.Roads.Where(r => r.Id == road.Id).FirstOrDefault();
            if (record != null)
            {
                record.Name = road.Name;
                record.Distance = road.Distance;
                record.StartingPoint = road.StartingPoint;
                record.EndingPoint = road.EndingPoint;

                return _context.SaveChanges() > 0;
            }


            return false;
        }
    }

}
