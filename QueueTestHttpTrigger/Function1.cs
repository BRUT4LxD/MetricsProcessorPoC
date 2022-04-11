using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace QueueTestHttpTrigger
{
    public static class Function1
    {
        public static readonly HttpClient httpClient = new();
        public const string Url = "https://queue-test-metrics-processor.azurewebsites.net/home";

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ExecutionContext context,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var response = await httpClient.GetAsync(Url);

            log.LogInformation(response.ToString());
            log.LogInformation(await response.Content.ReadAsStringAsync());
            log.LogInformation(response.StatusCode.ToString());

            return response.IsSuccessStatusCode ? new OkObjectResult("Success") : new BadRequestObjectResult("Failure");
        }
    }
}