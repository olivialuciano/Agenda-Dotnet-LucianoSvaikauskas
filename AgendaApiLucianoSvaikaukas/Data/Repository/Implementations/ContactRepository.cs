
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using AutoMapper;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(AgendaContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }

        public List<Contact> GetAllByUser(int userId)
        {
            return _context.Contacts.Where(c => c.UserId == userId).ToList();
        }
        //public List<Contact> GetAll()
        //{
        //    return _context.Contacts.ToList();
        //}

        public void Create(ContactForCreationDTO dto, int userId)
        {
            Contact contactoACargar = new Contact(){
                UserId = userId,
                CelularNumber = dto.CelularNumber,
                Description = dto.Description,
                Name = dto.Name,
                TelephoneNumber = dto.TelephoneNumber,
            };
            _context.Contacts.Add(contactoACargar);
            _context.SaveChanges();

        }

        public void Update(ContactForCreationDTO dto)
        {
            _context.Contacts.Update(_mapper.Map<Contact>(dto));
        }
        public void Delete(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Single(c => c.Id == id));
        }

        public List<Contact> GetAllByUser()
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        //public List<Contact> GetAllByUser()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
