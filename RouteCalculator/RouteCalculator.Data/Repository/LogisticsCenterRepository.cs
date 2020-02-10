using RouteCalculator.Data.Interfaces;
using RouteCalculator.Data.Models;
using Router.Domain;
using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using System.Linq;
using System.Text;
using EdgeList = System.Collections.Generic.List<(int node, double weight)>;

namespace RouteCalculator.Data.Repository
{
    public class LogisticsCenterRepository : ILogisticsCentersRepository
    {
        private readonly RouterContext _context;
        

        public LogisticsCenterRepository(RouterContext context)
        {
            this._context = context;
        }
        public List<LogisticsCenter> GetLogisticsCenter()
        {
            var centers = _context.LogisticsCenters.ToList();
            var result = new List<LogisticsCenter>();
            if (centers.Count != 0)
            {
                foreach (var item in centers)
                {
                    result.Add(new LogisticsCenter
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CalculationDate = item.CalculationDate
                    });
                }

            }

            return result;
        }
        public CalcResponse CalculateLogisticsCenter()
        {
            if ( PreCalculationCheck())
            {
                return CheckForNearestLocation(FindLongestRoute());
            }
            else
            {
                return new CalcResponse() { Message = "No changes since last creation", Status = false };
            }
            
        }


        public Result FindLongestRoute() 
        {
            var countOfLoccations = _context.Locations.Count();
            var  locationsList = _context.Locations.ToList();
            var arayOfRoads = _context.Roads.ToList();
            Graph graph = new Graph(countOfLoccations);
      

            foreach (var item in arayOfRoads)
            {
                graph.AddEdge(item.StartingPoint, item.EndingPoint, item.Distance);
            }
     
            var adjacencyList = graph.GetAdjacency();
            Result longestRoute = new Result() { startingPointId = 0, Distance = 0 };
            int index = 0;
            foreach (var items in adjacencyList)
            {
                
                if (items.Count != 0)
                {
                    index++;
                    var temp = items.OrderByDescending(item => item.weight).First();

                    if ( longestRoute.startingPointId != temp.node && longestRoute.startingPointId < temp.node)
                    {
                        if (longestRoute.nextPointId == index 
                            || longestRoute.nextPointId == temp.node )
                        {                           
                            longestRoute.nextPointId = temp.node;
                            longestRoute.Distance += temp.weight;
                        } 
                        else
                        {
                            longestRoute.Distance = 0;
                            longestRoute.startingPointId = index;
                            longestRoute.nextPointId = temp.node;
                            longestRoute.Distance += temp.weight;
                        }
                       
                    }
                   
                }
               
            }
            return longestRoute;
        }

        private CalcResponse CheckForNearestLocation(Result coordinates)
        {
            var location = DetermineLogisticsCenterLoc(coordinates);
            var isSaved = false;
            if (location != null)
            {
                var center = _context.LogisticsCenters.Select(x => x);
                _context.LogisticsCenters.RemoveRange(center);
                isSaved =  SaveLogisticsCenter(location);
            }
           
            var response = new CalcResponse() { };
            if (isSaved == true)
            {
                response.LocationId = location.Id;
                response.Message = "Logistics center is creted ";
                response.LocationName = location.Name;
                response.Status = true;
                response.dateCalculated = DateTime.Now;
            }
            else
            {
                response.LocationId = 0;
                response.Message = "Error occured while creating logistics center";
                response.LocationName = string.Empty;
                response.Status = false;
            }

            return response;
        }

        sealed class Graph
        {
            public readonly List<EdgeList> adjacency;

            public Graph(int vertexCount) => adjacency = Range(0, vertexCount).Select(v => new EdgeList()).ToList();

            public int Count => adjacency.Count;
            public bool HasEdge(int s, int e) => adjacency[s].Any(p => p.node == e);
            public bool RemoveEdge(int s, int e) => adjacency[s].RemoveAll(p => p.node == e) > 0;

            public List<EdgeList> GetAdjacency()
            {
                return adjacency;
            }

            public bool AddEdge(int s, int e, double weight)
            {
                if (HasEdge(s, e)) return false;
                adjacency[s].Add((e, weight));
                return true;
            }

            public (double distance, int prev)[] FindPath(int start)
            {
                var info = Range(0, adjacency.Count).Select(i => (distance: double.NegativeInfinity, prev: i)).ToArray();
                info[start].distance = 0;
                var visited = new System.Collections.BitArray(adjacency.Count);

                var heap = new Heap<(int node, double distance)>((a, b) => a.distance.CompareTo(b.distance));
                heap.Push((start, 0));
                while (heap.Count > 0)
                {
                    var current = heap.Pop();
                    if (visited[current.node]) continue;
                    var edges = adjacency[current.node];
                    for (int n = 0; n < edges.Count; n++)
                    {
                        int v = edges[n].node;
                        if (visited[v]) continue;
                        double alt = info[current.node].distance + edges[n].weight;
                        if (alt > info[v].distance)
                        {
                            info[v] = (alt, current.node);
                            heap.Push((v, alt));
                        }
                    }
                    visited[current.node] = true;
                }
                return info;
            }

        }

        sealed class Heap<T>
        {
            private readonly IComparer<T> comparer;
            private readonly List<T> list = new List<T> { default };

            public Heap() : this(default(IComparer<T>)) { }

            public Heap(IComparer<T> comparer)
            {
                this.comparer = comparer ?? Comparer<T>.Default;
            }

            public Heap(Comparison<T> comparison) : this(Comparer<T>.Create(comparison)) { }

            public int Count => list.Count - 1;

            public void Push(T element)
            {
                list.Add(element);
                SiftUp(list.Count - 1);
            }

            public T Pop()
            {
                T result = list[1];
                list[1] = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                SiftDown(1);
                return result;
            }

            private static int Parent(int i) => i / 2;
            private static int Left(int i) => i * 2;
            private static int Right(int i) => i * 2 + 1;

            private void SiftUp(int i)
            {
                while (i > 1)
                {
                    int parent = Parent(i);
                    if (comparer.Compare(list[i], list[parent]) > 0) return;
                    (list[parent], list[i]) = (list[i], list[parent]);
                    i = parent;
                }
            }

            private void SiftDown(int i)
            {
                for (int left = Left(i); left < list.Count; left = Left(i))
                {
                    int smallest = comparer.Compare(list[left], list[i]) <= 0 ? left : i;
                    int right = Right(i);
                    if (right < list.Count && comparer.Compare(list[right], list[smallest]) <= 0) smallest = right;
                    if (smallest == i) return;
                    (list[i], list[smallest]) = (list[smallest], list[i]);
                    i = smallest;
                }
            }

        }

        public class Result
        {
            public int startingPointId { get; set; }
            public int nextPointId { get; set; }
            public double Distance { get; set; }
        }

        public  Location DetermineLogisticsCenterLoc(Result coordinates)
        {
            var result = _context.Roads.Where(r => r.StartingPoint == coordinates.nextPointId
           || r.EndingPoint == coordinates.nextPointId).OrderByDescending(x => x.Distance).Last();
            Location center = null;

            if (result.StartingPoint == coordinates.nextPointId)
            {
                center = _context.Locations.Where(x => x.Id == result.EndingPoint).FirstOrDefault();
            }
            else
            {
                center =  _context.Locations.Where(x => x.Id == result.StartingPoint).FirstOrDefault();
            }

            
            return center;
        }

        public bool SaveLogisticsCenter(Location loc) 
        {
            _context.LogisticsCenters.Add(new LogisticsCenter() { RoadId = loc.Id,Name = loc.Name, CalculationDate = DateTime.Now});
            return _context.SaveChanges() > 0;        
        }

        public bool PreCalculationCheck()
        {
            var isCalculationRequired = false;

            var logisticsCenter = _context.LogisticsCenters.FirstOrDefault();
            if (logisticsCenter != null)
            {
                if (_context.Roads.Where(l => l.CalculatonDate > logisticsCenter.CalculationDate).FirstOrDefault() != null)
                {
                    isCalculationRequired = true;
                }
            }
            else
            {
                isCalculationRequired = true;
            }
            
            return isCalculationRequired;
        }


    }
}
