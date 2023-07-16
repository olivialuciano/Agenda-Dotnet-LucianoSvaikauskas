using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        public void CreateGroup(Group group);
        public Group GetGroupById(int groupId);
        //public void AddContactsToGroup(int groupId, List<int> contactIds)
        //public Group CreateGroup(Group group, List<int> userIds);
        public List<Group> GetAll(int userId);
        public List<Group> GetAllByUser(int userId);
        //public void Create(GroupForCreationDTO dto, int userId);
        //public void Update(GroupForCreationDTO dto, int userId, int groupId);
        public void Delete(int id, int userId);
        public void UpdateGroupName(int groupId, string newName);
    }
}
