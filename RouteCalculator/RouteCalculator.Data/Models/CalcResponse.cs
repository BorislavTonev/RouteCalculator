using System;
using System.Collections.Generic;
using System.Text;

namespace RouteCalculator.Data.Models
{
    public class CalcResponse
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

        public DateTime  dateCalculated { get; set; }
    }
}
