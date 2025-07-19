namespace AIGenerator.Models
{
    public class Service
    {
        public short serviceId { get; set; }

        public string? serviceName { get; set; }

        public string? ServiceDescription { get; set; }

        public byte[]? serviceImageFile { get; set; }

        public string? serviceImageName { get; set; }

        public string? serviceImageContentType { get; set; }

        public bool isDeleted { get; set; }

        public List<Project> projects { get; set; }

        public List<PortfolioService> portfolioServices { get; set; }
    }
}
