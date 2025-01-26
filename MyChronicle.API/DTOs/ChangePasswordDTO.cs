using System.ComponentModel.DataAnnotations;

namespace MyChronicle.API.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
