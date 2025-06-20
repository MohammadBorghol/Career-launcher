using System.ComponentModel.DataAnnotations;

namespace AIGenerator.Models
{
    public class PersonalInfo
    {
        
        public string? firstName { get; set; }

        public string? secondName { get; set; }

        public string? thirdName { get; set; }
       
        public string? lastName { get; set; }

        [EmailAddress]
        public string? email { get; set; }
        
        public string? phoneNumber { get; set; }

        [Url]
        public string? linkedInLink { get; set; }
        [Url]
        public string? githubLink { get; set; }
        [Url]
        public string? facebookLink { get; set; }
        
        public string? address { get; set; }

        public string? DateOfBirth { get; set; }

        public string? summary { get; set; }

        public string? title { get; set; }


    }
}
