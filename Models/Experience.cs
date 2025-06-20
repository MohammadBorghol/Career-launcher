namespace AIGenerator.Models
{
    public class Experience
    {
        public int experienceId { get; set; }

        public string? title { get; set; }

        public string? companyName { get; set; }

        public string? startDate { get; set; }

        public string? endDate { get; set; }

        public bool isCurrent { get; set; }

        public List<string>? duties { get; set; }

        public Resume resume { get; set; }

        public int resumeId { get; set; }





    }
}
