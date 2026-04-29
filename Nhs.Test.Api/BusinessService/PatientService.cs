using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Nhs.Test.Api.Model;
using System.Threading.Tasks;

namespace Nhs.Test.Api.BusinessService
{
    public class PatientService(IPatientRepository patientRepository, ICacheService cacheService, IMapper mapper) : IPatientService
    {
        public async Task<PatientDTO> GetByIdAsync(int id)
        {

            var item = await cacheService.GetByIdAsync(id);

            if (item is null)
            {
                item = await patientRepository.GetByIdAsync(id);
                await cacheService.SetAsync(item);
            }

            var result = mapper.Map<PatientDTO>(item);

            return result;
        }

        public async Task<List<PatientDTO>> ListAsync()
        {
            var data = await patientRepository.ListAsync();
            var results = mapper.Map<List<PatientDTO>>(data);
            return results;
        }
    }
}
