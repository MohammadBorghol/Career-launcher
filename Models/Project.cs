namespace AIGenerator.Models
{
        public class Project
        {

            public int projectId { get; set; }

            public string? ProjectName { get; set; }

            public string? projectDescription { get; set; }

            public string? startDate { get; set; }

            public string? endDate { get; set; }

            public Service service { get; set; }

            public short? serviceId { get; set; }

            public Portfolio portfolio { get; set; }

            public int? portfolioId { get; set; }

            public string? projectLink { get; set; }

            public bool isDeleted { get; set; }

            public byte[]? projectImageFile { get; set; }

            public string? projectImageName { get; set; }

            public string? projectImageContentType { get; set; }

    }
}
