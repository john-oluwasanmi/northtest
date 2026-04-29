using Nhs.Test.Api.Model;

namespace Nhs.Test.Api.BusinessService
{
    public interface IPatientService
    {
        Task<PatientDTO> GetByIdAsync(int id);
        Task<List<PatientDTO>> ListAsync();
    }
}