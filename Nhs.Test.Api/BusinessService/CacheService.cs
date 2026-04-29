using Microsoft.Extensions.Caching.Memory;
using Nhs.Test.Api.Model;
using System.Threading.Tasks;

namespace Nhs.Test.Api.BusinessService
{
    public class CacheService(IMemoryCache _cache) : ICacheService
    {
        public async Task<Patient> GetByIdAsync(int id)
        {
            string cacheKey = $"{nameof(Patient)}_{id}";
            _cache.TryGetValue(cacheKey, out Patient patient);

            return patient;
        }

        public async Task SetAsync(Patient patient)
        {
            string cacheKey = $"{nameof(Patient)}_{patient.Id}";

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            _cache.Set(cacheKey, patient, cacheOptions);
        }
    }
}
