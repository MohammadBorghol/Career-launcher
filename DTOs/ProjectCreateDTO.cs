using AIGenerator.Models;

namespace AIGenerator.DTOs
{
    public class ProjectCreateDTO
    {

        public string? ProjectName { get; set; }

        public string? projectDescription { get; set; }

        public string? startDate { get; set; }

        public string? endDate { get; set; }

        public Service service { get; set; }

        public short? serviceId { get; set; }

        public Portfolio portfolio { get; set; }

        public int? portfolioId { get; set; }

        public string? projectLink { get; set; }

        public IFormFile? projectImage { get; set; }
    }
}
