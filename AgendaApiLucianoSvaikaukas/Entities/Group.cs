using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AgendaApiLucianoSvaikaukas.Entities
{
    public class Group
    {
        //public Group()
        //{
        //    Contacts = new List<Contact>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; }
    }
}

