namespace AIGenerator.Models
{
    public class Resume : PersonalInfo
    {

        public int resumeId { get; set; }

        public string createDate { get; set; }

        public string ModifiedDate { get; set; }

        public List<Education> educations { get; set; }

        public List<Experience> experiences { get; set; }

        public List<Skill> skills { get; set; }

        public List<Language> languages { get; set; }

        public List<Certificate> certificates { get; set; }

        public EndUser endUser { get; set; }

        public string endUserId { get; set; }

        public bool isDeleted { get; set; }

        public string userPrompt { get; set; }
    }
}
