using System;
using System.Collections.Generic;
using System.Text;

namespace Router.Domain
{
    public class LogisticsCenter
    {
        public int Id { get; set; }
        public int RoadId { get; set; }
        public DateTime CalculationDate { get; set; }
        public string Name { get; set; }

    }
}
