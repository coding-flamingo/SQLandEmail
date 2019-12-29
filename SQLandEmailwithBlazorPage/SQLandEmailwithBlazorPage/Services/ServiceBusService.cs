using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SQLandEmailwithBlazorPage.Services
{
    public class ServiceBusService
    {
        static IQueueClient queueClient;
        public static async Task<bool> SendMessagesAsync(string ServiceBusConnectionString, string QueueName, string toEmail, string Subject, string MessageBody)
        {
            if (string.IsNullOrEmpty(MessageBody) || string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(QueueName)
                || string.IsNullOrEmpty(ServiceBusConnectionString) || string.IsNullOrEmpty(Subject))
            {
                return false;
            }
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            var message = new Message(Encoding.UTF8.GetBytes(MessageBody));
            message.To = toEmail;
            message.Label = Subject.Trim();

            await queueClient.SendAsync(message);
            await queueClient.CloseAsync();
            return true;
        }
    }
}
