using System.ComponentModel.DataAnnotations;

namespace AgendaApiLucianoSvaikaukas.Models
{
    public class AuthenticationRequestBody
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
