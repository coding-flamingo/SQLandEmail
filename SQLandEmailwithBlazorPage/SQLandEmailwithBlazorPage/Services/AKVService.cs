using Azure.Security.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace SQLandEmailwithBlazorPage.Services
{
    public class AKVService : IAKVService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly SecretClient _akvSecretClient;
        private readonly string _akvURL;
        public AKVService(IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _akvURL = configuration.GetSection("ServiceBus").GetValue<string>("akv");  
            _akvSecretClient = new SecretClient(new Uri(_akvURL), new DefaultAzureCredential());
            _memoryCache = memoryCache;
        }

        public async Task<string> GetKeyVaultSecretAsync(string secretName)
        {
            string secret;
            if (!_memoryCache.TryGetValue(secretName, out secret))
            {
                KeyVaultSecret returnedSecret = await _akvSecretClient.GetSecretAsync(secretName);
                secret = returnedSecret.Value;
                _memoryCache.Set(secretName, secret, DateTimeOffset.UtcNow.AddHours(1));
            }
            return secret;
        }
    }
    public interface IAKVService
    {
        public Task<string> GetKeyVaultSecretAsync(string secretName);
    }
}
