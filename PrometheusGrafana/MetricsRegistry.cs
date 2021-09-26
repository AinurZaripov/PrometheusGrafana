using App.Metrics.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusGrafana
{
    public class MetricsRegistry
    {
        public static CounterOptions CreatedCustomerCounter => new CounterOptions
        {
            Name = "Created Customer",
            Context = "CustomerApi",
            MeasurementUnit = App.Metrics.Unit.Calls
        };
    }
}
