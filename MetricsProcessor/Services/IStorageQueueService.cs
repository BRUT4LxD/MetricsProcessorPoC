using MetricsProcessor.DTO;

namespace MetricsProcessor.Services
{
    public interface IStorageQueueService
    {
        Task<NotificationModel> Dequeue(string queueName);
    }
}