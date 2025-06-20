namespace AIGenerator.Models
{
    public class Language
    {
        public short languageId { get; set; }

        public string? languageName { get; set; }

        public string? level { get; set; }

        public Resume resume { get; set; }

        public int resumeId { get; set; }

    }
}
