using System;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using MetricsProcessor.DTO;

namespace MetricsProcessor.Services
{
    public class StorageQueueService : IStorageQueueService
    {
        private string _queueConnectionString;

        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public StorageQueueService(IConfiguration config)
        {
            _queueConnectionString = config.GetValue<string>("Storage:Queue:ConnectionString");
        }

        public async Task<NotificationModel> Dequeue(string queueName)
        {
            QueueClient queueClient = new QueueClient(_queueConnectionString, queueName);

            var message = await queueClient.PeekMessageAsync();

            var messageBodyString = message?.Value?.Body?.ToString() ?? "";

            return JsonSerializer.Deserialize<NotificationModel>(messageBodyString, _serializerOptions);
        }
    }
}