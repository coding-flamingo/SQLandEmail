using Microsoft.Extensions.Configuration;
using SQLandEmailwithBlazorPage.Models;
using SQLandEmailwithBlazorPage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLandEmailwithBlazorPage.Managers
{
    public class EmailManager
    {
        private readonly string _connectionString;
        private readonly string _queue;
        private readonly AKVService _keyVaultService;
        public EmailManager(IConfiguration configuration)
        {
            _keyVaultService = new AKVService();
            _connectionString = _keyVaultService.GetKeyVaultSecret(configuration.GetSection("ServiceBus").GetValue<string>("ServiceBusCS"));
            _queue = configuration.GetSection("ServiceBus").GetValue<string>("QueueName");
        }
        public async Task SendWelcomeEmailAsync(PersonModel user)
        {
            string subject = "Welcome to my Page";
            string body = "Hi " + user.FirstName + " " + user.LastName + '\n' + "Welcome to our page we are glad to have you here \n\n\n Coding Flamingo";
            try
            {
                var successful = await ServiceBusService.SendMessagesAsync(_connectionString, _queue, user.Email, subject, body);
                if (!successful)
                {
                    throw new Exception("Could not send Email");
                }
            }
            catch
            {
            }

        }
    }
}
