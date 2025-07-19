using System.ComponentModel.DataAnnotations;

namespace AIGenerator.DTOs
{
    public class ServiceUpdateDTO
    {
        [Required]
        public short ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; } = null!;

        public string? ServiceDescription { get; set; }

        public IFormFile? ServiceImage { get; set; }
    }
}
