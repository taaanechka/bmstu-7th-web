#nullable disable

using System.ComponentModel.DataAnnotations;

namespace API
{
    public class UserToAdd
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserType { get; set; }
    }
}
