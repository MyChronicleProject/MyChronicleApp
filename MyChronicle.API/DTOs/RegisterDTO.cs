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
        public string FirstName {  get; set; }
        [Required]
        public string LastName { get; set; }
        public string UserName { get => FirstName + " " + LastName; }
    }
}
