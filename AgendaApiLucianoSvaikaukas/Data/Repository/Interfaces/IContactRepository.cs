
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IContactRepository
    {

        ////////// GET //////////
        public Contact GetContactById(int contactId);
        public List<Contact> GetAll(int userId);
        public List<Contact> GetAllByUser(int userId);

        ////////// POST //////////
        public void Create(ContactForCreationDTO dto, int userId);

        ////////// PUT //////////
        public void Update(ContactForCreationDTO dto, int userId, int id);

        ////////// DELETE //////////
        public void Delete(int id, int userId);

    }
}
