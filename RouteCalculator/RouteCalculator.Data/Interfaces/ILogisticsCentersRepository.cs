using RouteCalculator.Data.Models;
using Router.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouteCalculator.Data.Interfaces
{
    public interface ILogisticsCentersRepository
    {
        List<LogisticsCenter> GetLogisticsCenter();
        CalcResponse CalculateLogisticsCenter();
    }
}
