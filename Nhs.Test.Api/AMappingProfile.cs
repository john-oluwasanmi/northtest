using AutoMapper;
using Nhs.Test.Api.Model;

namespace Nhs.Test.Api.Mapping
{
    public class AMappingProfile : Profile
    {
        public AMappingProfile()
        {
            CreateMap<Patient, PatientDTO>()
                .ReverseMap();
        }
    }
}
