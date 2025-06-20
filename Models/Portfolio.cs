namespace AIGenerator.Models
{
    public class Portfolio : PersonalInfo
    {

        public int portfolioId { get; set; }

        public byte[]? portfolioImageFile { get; set; }

        public string? portfolioImageName { get; set; }

        public string? portfolioImageContentType { get; set; }

        public List<Project> projects { get; set; } = new();

        public string? createDate { get; set; }

        public string? ModifiedDate { get; set; }

        public EndUser endUser { get; set; }

        public string endUserId { get; set; }

        public bool isDeleted { get; set; }

        public string? userPrompt { get; set; }

        public List<PortfolioService> portfolioServices { get; set; } = new();


    }
}
