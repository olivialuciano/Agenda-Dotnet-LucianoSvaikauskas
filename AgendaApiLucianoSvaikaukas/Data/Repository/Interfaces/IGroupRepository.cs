using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        public void CreateGroup(Group group);
        public Group GetGroupById(int groupId);
        public List<Group> GetAll(int userId);
        public List<Group> GetAllByUser(int userId);
        public void Delete(int id, int userId);
        public void UpdateGroupName(int groupId, string newName);

        public void AddContact(ContactForAssignGroupDTO dto);
    }
}
