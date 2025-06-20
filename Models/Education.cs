using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AIGenerator.Models
{
    public class Education
    {
        public int educationId { get; set; }

        public string? collegeName { get; set; }

        public string?  degreeType { get; set; }

        public string? startDate { get; set; }

        public string? endDate { get; set; }

        public string? majorName { get; set; }

        public double? GPA{ get; set; }

        public Resume resume { get; set; }

        public int resumeId { get; set; }

    }
}
