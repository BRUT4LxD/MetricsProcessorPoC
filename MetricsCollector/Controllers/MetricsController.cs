using MetricsCollector.DTO;
using MetricsCollector.Events;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace MetricsCollector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly TelemetryClient _telemetryClient;

        public MetricsController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        [HttpPost("notifications", Name = "NotificationsMetric")]
        public void Get([FromBody] NotificationModel notificationModel)
        {
            _telemetryClient.TrackEvent(NotificationEvents.NotificationGet);
        }
    }
}