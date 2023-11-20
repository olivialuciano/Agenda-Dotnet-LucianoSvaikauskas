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


        ////////// GET //////////

        public GroupWithContactsDTO GetGroupById(int groupId)
        {
            var group = _context.Groups
                .Include(g => g.Contacts)
                .FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                return null; 
            }

            // Mapear el objeto Group a un DTO para evitar problemas de referencia circular (error q nos tiraba)
            var groupDTO = new GroupWithContactsDTO
            {
                Id = group.Id,
                Name = group.Name,
                Contacts = group.Contacts.Select(contact => new ContactDTO
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    CelularNumber = contact.CelularNumber,
                    Description = contact.Description,
                    TelephoneNumber = contact.TelephoneNumber
                }).ToList()
            };

            return groupDTO;
        }

        public List<Group> GetAllByUser(int userId)
        {
            return _context.Groups.Where(g => g.UserId == userId).ToList();
        }



        ////////// POST //////////

        public void CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public void AddContact(ContactForAssignGroupDTO dto)
        {
            var contact = _context.Contacts
                .Where(c => c.Id == dto.ContactId)
                .Include(c => c.Groups)
                .FirstOrDefault();
            var group = _context.Groups.Find(dto.GroupId);
            contact.Groups.Add(group);
            _context.SaveChanges();
        }



        ////////// PUT //////////

        public void UpdateGroupName(int groupId, string newName)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == groupId);

            if (group != null)
            {
                group.Name = newName;
                _context.SaveChanges();
            }
        }


        ////////// DELETE //////////

        public void Delete(int id, int userId)
        {
            _context.Groups.Remove(_context.Groups.Single(c => c.Id == id && c.UserId == userId));
            _context.SaveChanges();
        }


    }
}
