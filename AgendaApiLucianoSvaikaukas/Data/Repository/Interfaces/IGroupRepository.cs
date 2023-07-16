using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        public List<Group> GetAll(int userId);
        public List<Group> GetAllByUser(int userId);
        public void Create(GroupForCreationDTO dto, int userId);
        public void Update(GroupForCreationDTO dto, int userId, int groupId);
        public void Delete(int id, int userId);
    }
}
