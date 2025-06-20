using AIGenerator.Models;

namespace AIGenerator.DTOs
{
    public class ResumeUpdateDTO
    {

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string? linkedInLink { get; set; }

        public string? githubLink { get; set; }

        public string? facebookLink { get; set; }

        public string address { get; set; }


        public int resumeId { get; set; }       

        public string userPrompt { get; set; }

        public string userInput { get; set; }
    }
}
