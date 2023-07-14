using AgendaApiLucianoSvaikaukas.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaApiLucianoSvaikaukas.Models
{
    public class ContactForCreationDTO
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        public long CelularNumber { get; set; }
        public long? TelephoneNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        
        //public ICollection<Group>? Groups { get; set; }
    }

}
