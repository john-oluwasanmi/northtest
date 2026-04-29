using Microsoft.EntityFrameworkCore;
using Nhs.Test.Api.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nhs.Test.Api.BusinessService
{
    public class PatientRepository(AppDbContext appDbContext) : IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int id)
        {

            return await appDbContext.Patients.FindAsync(id);
        }

        public async Task<List<Patient>> ListAsync()
        {
            return await appDbContext.Patients.ToListAsync();
        }
    }
}
