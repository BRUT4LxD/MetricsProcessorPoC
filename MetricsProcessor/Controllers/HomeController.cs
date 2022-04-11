using System.Net.Http.Json;
using Azure.Storage.Queues.Models;
using MetricsProcessor.DTO;
using MetricsProcessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string MetricsCollectorUrl = "https://metrics-collector.azurewebsites.net/metrics/notifications";
        private readonly IStorageQueueService _storageQueueService;
        private const string QueueName = "queue-storage-mp";

        public HomeController(IHttpClientFactory httpClientFactory, IStorageQueueService storageQueueService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _storageQueueService = storageQueueService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var queueElement = await _storageQueueService.Dequeue(QueueName);
            _ = Task.Run(() => _httpClient.PostAsJsonAsync(MetricsCollectorUrl, queueElement));
            return Ok("success");
        }
    }
}