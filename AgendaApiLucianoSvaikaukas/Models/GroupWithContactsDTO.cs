namespace AgendaApiLucianoSvaikaukas.Models
{
    public class GroupWithContactsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContactDTO> Contacts { get; set; }
    }
}
