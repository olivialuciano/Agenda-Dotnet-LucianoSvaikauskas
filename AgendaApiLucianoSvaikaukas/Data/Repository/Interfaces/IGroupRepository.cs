using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        public List<Group> GetAll();
        public void Create(GroupForCreationDTO dto);
        public void Update(GroupForCreationDTO dto);
        public void Delete(int id);
    }
}
