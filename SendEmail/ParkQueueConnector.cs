namespace SendEmail
{
    using System;
    using System.Threading.Tasks;
    using Azure.Storage.Queues;
    using Coravel;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Formatting.Json;

    public class ParkQueueConnector : IOrderConnector
    {
        private readonly ILogger<ParkQueueConnector> logger;
        private readonly QueueClient orderQueueClient;
        private readonly QueueClient poisonOrderQueueClient;

        public ParkQueueConnector(ILogger<ParkQueueConnector> logger)
        {
            this.logger = logger;

            var connectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION");
            var queueOpts = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };

            orderQueueClient = new QueueClient(connectionString, "customer-orders", queueOpts);
            orderQueueClient.CreateIfNotExists();

            poisonOrderQueueClient = new QueueClient(connectionString, "customer-orders-poison", queueOpts);
            poisonOrderQueueClient.CreateIfNotExists();
        }

        public ParkQueueConnector()
        {
        }

        public async Task<ParkInfo> GetNextOrder()
        {
            var response = await orderQueueClient.ReceiveMessageAsync();

            if (response.Value != null)
            {
                try
                {
                    var order = response.Value.Body.ToObjectFromJson<ParkInfo>();
                    order.QueueMessageId = response.Value.MessageId;
                    order.QueuePopReceipt = response.Value.PopReceipt;

                    return order;
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, "Error deserializing message", response.Value);

                    await poisonOrderQueueClient.SendMessageAsync(response.Value.Body);
                    await orderQueueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                }
            }

            return null;
        }

        public async Task RemoveOrder(ParkInfo order)
        {
            await orderQueueClient.DeleteMessageAsync(order.QueueMessageId, order.QueuePopReceipt);
        }

        public IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    var smtpHost = Environment.GetEnvironmentVariable("Outlook");
                    var smtpUser = Environment.GetEnvironmentVariable("");
                    var smtpPass = Environment.GetEnvironmentVariable("");

                    services
                        .AddFluentEmail("ms@axdata.com")
                        .AddRazorRenderer()
                        .AddSmtpSender(smtpHost, 587, smtpUser, smtpPass);

                    services.AddSingleton<IOrderConnector, ParkQueueConnector>();
                    services.AddScheduler();
                    services.AddTransient<ProcessPark>();
                });
        }
    }
}
