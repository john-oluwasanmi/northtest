using System.ComponentModel.DataAnnotations;

namespace Nhs.Test.Api.Model
{


    public class Patient
    {
        [Required]
        public int Id { get; set; }
        public string NHSNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public string GPPractice { get; set; }
    }
}
