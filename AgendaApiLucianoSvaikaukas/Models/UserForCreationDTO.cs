using System.ComponentModel.DataAnnotations;

namespace AgendaApiLucianoSvaikaukas.Models
{
    public class UserForCreationDTO
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
