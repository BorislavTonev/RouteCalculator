using System;
using System.Collections.Generic;
using System.Text;

namespace Router.Domain
{
    public class Road
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int StartingPoint { get; set; }
        public int EndingPoint { get; set; }
        public double Distance { get; set; }
        public DateTime CalculatonDate { get; set; }
    }
}
