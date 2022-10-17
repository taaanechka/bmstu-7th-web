#nullable disable

using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class UserToLogin
    {
        [Required]
        public string Login {get; set;}
        [Required]
        public string Password {get; set;}
    }
}
