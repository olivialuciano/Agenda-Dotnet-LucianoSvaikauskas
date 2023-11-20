using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        ////////// GET //////////
        public GroupWithContactsDTO GetGroupById(int groupId);

        //public List<Group> GetAll(int userId); No necesitamos getall de groups xq es x user.
        public List<Group> GetAllByUser(int userId);

        ////////// POST //////////
        public void CreateGroup(Group group);
        public void AddContact(ContactForAssignGroupDTO dto);

        ////////// PUT //////////
        public void UpdateGroupName(int groupId, string newName);

        ////////// DELETE //////////
        public void Delete(int id, int userId);

    }
}
