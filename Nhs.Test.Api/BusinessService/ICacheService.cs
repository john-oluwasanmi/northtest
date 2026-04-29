using Nhs.Test.Api.Model;

namespace Nhs.Test.Api.BusinessService
{
    public interface ICacheService
    {
        Task<Patient> GetByIdAsync(int id);
        Task SetAsync(Patient patient);
    }
}