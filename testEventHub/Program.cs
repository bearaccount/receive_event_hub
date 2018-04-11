using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace testEventHub
{
    class Program
    {
        private const string EhConnectionString = "Endpoint=sb://oseventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YwRx2oOuw4JIx8C3AKq0qRWRa8j6Urj8ax21u93vKxk="; //Event Hubs connection string
        private const string EhEntityPath = "HoloLensCmds"; // Event Hub path/name
        private const string StorageContainerName = "azure-webjobs-hosts"; // Storage account container name
        private const string StorageAccountName = "ostestfunctionaaf3e"; //{Storage account name} 
        private const string StorageAccountKey = "7evtrANvFHn4hSFgLpGUGyA2WtzUJGBxOG7/s28lfbYxTPJbyrHIR5zEm84xksj4rIzLZIa6Kikofhr5kJXHsQ=="; // Storage account key

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EhEntityPath,
                PartitionReceiver.DefaultConsumerGroupName,
                EhConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
