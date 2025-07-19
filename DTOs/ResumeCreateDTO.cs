using AIGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace AIGenerator.DTOs
{
    public class ResumeCreateDTO
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phoneNumber { get; set; }

        public string githubLink { get; set; }

        public string facebookLink { get; set; }

        public string linkedInLink { get; set; }

        public string userPrompt { get; set; }

        public string userInput { get; set; }


    }
}
