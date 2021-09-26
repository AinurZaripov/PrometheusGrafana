using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusGrafana.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ILogger<CustomerController> _logger;
    private readonly IMetrics _metrics;
    public CustomerController(ILogger<CustomerController> logger, IMetrics metrics)
    {
      _logger = logger;
      _metrics = metrics;
    }

    // POST api/<CustomerController>
    [HttpPost]
    public void Post()
    {
      _metrics.Measure.Counter.Increment(MetricsRegistry.CreatedCustomerCounter);
    }
  }
}
