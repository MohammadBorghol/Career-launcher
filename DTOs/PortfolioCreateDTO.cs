using AIGenerator.Models;

namespace AIGenerator.DTOs
{
    public class PortfolioCreateDTO
    {

        public IFormFile? portfolioImage { get; set; }

        public List<IFormFile>? projectImages { get; set; }

        public List<short> serviceIds { get; set; }

        public List<Project> projects { get; set; }

        public string? userPrompt { get; set; }

    }
}
