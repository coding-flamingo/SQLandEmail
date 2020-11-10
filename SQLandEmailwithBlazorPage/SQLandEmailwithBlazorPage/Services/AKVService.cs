using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLandEmailwithBlazorPage.Services
{
    public class AKVService : IAKVService
    {
        private readonly AzureServiceTokenProvider _azureTokenProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly KeyVaultClient _akvClient;
        public AKVService(AzureServiceTokenProvider tokenProvider)
        {
            _azureTokenProvider = tokenProvider;
            _akvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                _azureTokenProvider.KeyVaultTokenCallback));
        }

        public async Task<string> GetKeyVaultSecretAsync(string secretName)
        {
            string secret;
            if (!_memoryCache.TryGetValue(secretName, out secret))
            {
                secret = (await _akvClient.GetSecretAsync(secretName)).Value;
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
