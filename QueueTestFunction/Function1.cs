using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace QueueTestFunction
{
    public class Function1
    {
        public readonly HttpClient httpClient = new();
        public const string Url = "https://queue-test-metrics-processor.azurewebsites.net/home";

        [FunctionName("MetricProcessTrigger")]
        public Task Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            ConfigurationBuilder b = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            return httpClient.GetAsync(Url);
        }
    }
}