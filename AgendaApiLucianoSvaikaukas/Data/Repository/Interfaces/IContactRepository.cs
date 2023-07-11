
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        public List<Contact> GetAll();
        public void Create(ContactForCreationDTO dto);
        public void Update(ContactForCreationDTO dto);
        public void Delete(int id);
    }
}
