using System.ComponentModel.DataAnnotations;

namespace MyChronicle.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
