
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        public List<Contact> GetAll();
        public List<Contact> GetAllByUser(int userId);
        public void Create(ContactForCreationDTO dto, int userId);
        public void Update(ContactForCreationDTO dto);
        public void Delete(int id);
    }
}
