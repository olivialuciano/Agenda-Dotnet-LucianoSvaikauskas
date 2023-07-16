using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;

        public GroupRepository(AgendaContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }



        public Group GetGroupById(int groupId)
        {
            return _context.Groups.FirstOrDefault(g => g.Id == groupId);
        }



        public void CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }



        public void Delete(int id, int userId)
        {
            _context.Groups.Remove(_context.Groups.Single(c => c.Id == id && c.UserId == userId));
            _context.SaveChanges();
        }


        public List<Group> GetAllByUser(int userId)
        {
            return _context.Groups.Where(g => g.UserId == userId).ToList();
        } //agregué prop userId en group

        public List<Group> GetAll(GroupForCreationDTO dto, int userId)
        {
            return _context.Groups.ToList();
        }



        //public void Update(GroupForCreationDTO dto, int userId, int groupId)
        //{
        //    var grupoAModificar = _context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == groupId);

        //    if (grupoAModificar != null)
        //    {
        //        grupoAModificar.UserId = userId;
        //        grupoAModificar.Id = groupId;
        //        grupoAModificar.Name = dto.Name;
        //        //contactoAModificar.Contacts = dto.Contacts;

        //        _context.SaveChanges();
        //    }
        //}

        public void UpdateGroupName(int groupId, string newName)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == groupId);

            if (group != null)
            {
                group.Name = newName;
                _context.SaveChanges();
            }
        }

        public List<Group> GetAll(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
