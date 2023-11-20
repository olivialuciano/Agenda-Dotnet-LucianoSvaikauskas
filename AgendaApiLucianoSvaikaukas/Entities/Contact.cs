using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AgendaApiLucianoSvaikaukas.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public long CelularNumber { get; set; }
        public long? TelephoneNumber { get; set; }
        public string Description { get; set; } = string.Empty; //inicializamos en vacío (otra forma)
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        
        [JsonIgnore]
        public ICollection<Group> Groups { get; set; }
    }
}
