using Nhs.Test.Api.Model;

namespace Nhs.Test.Api.BusinessService
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);
        Task<List<Patient>> ListAsync();
    }
}