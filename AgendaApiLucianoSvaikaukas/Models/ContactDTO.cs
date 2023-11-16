namespace AgendaApiLucianoSvaikaukas.Models
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long CelularNumber { get; set; }
        public string Description { get; set; }
        public long? TelephoneNumber { get; set; }
    }
}
