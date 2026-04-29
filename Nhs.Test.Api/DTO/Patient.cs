namespace Nhs.Test.Api.Model
{


    public class PatientDTO
    {
        public int Id { get; set; }
        public string NHSNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GPPractice { get; set; }
    }
}
