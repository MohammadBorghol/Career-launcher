using Microsoft.AspNetCore.Identity;

namespace AIGenerator.Models
{
    public class Person: IdentityUser
    {
        public string? fristName { get; set; }

        public string? lastName { get; set; }

    }
}
