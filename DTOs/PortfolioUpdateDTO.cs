using AIGenerator.Models;

namespace AIGenerator.DTOs
{
    public class PortfolioUpdateDTO
    {

        public string firstName { get; set; }

        public string? secondName { get; set; }

        public string? thirdName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string? linkedInLink { get; set; }

        public string? githubLink { get; set; }

        public string? facebookLink { get; set; }

        public string address { get; set; }

        public string summary { get; set; }      

        public int portfolioId { get; set; }        

        public string services { get; set; }

        public string projects { get; set; }

        public string userPrompt { get; set; }

        public List<short> serviceIds { get; set; }

        public List<Project> Projects { get; set; } = new();

        public string userInput { get; set; }

        public IFormFile? portfolioImage { get; set; }
    }
}
