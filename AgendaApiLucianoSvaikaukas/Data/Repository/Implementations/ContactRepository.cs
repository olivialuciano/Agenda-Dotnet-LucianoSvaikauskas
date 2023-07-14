
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
        public List<Contact> GetAll(int userId)
        {
            return _context.Contacts.ToList();
        }

        public void Create(ContactForCreationDTO dto, int userId)
        {
            Contact contactoACargar = new Contact(){
                UserId = userId,
                CelularNumber = dto.CelularNumber,
                Description = dto.Description,
                Name = dto.Name,
                TelephoneNumber = dto.TelephoneNumber,
                //Groups = dto.Groups,
            };
            _context.Contacts.Add(contactoACargar);
            _context.SaveChanges();

        }
        public void Update(ContactForCreationDTO dto, int userId, int contactId)
        {
            var contactoAModificar = _context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == contactId);

            if (contactoAModificar != null)
            {
                contactoAModificar.UserId = userId;
                contactoAModificar.Id = contactId;
                contactoAModificar.CelularNumber = dto.CelularNumber;
                contactoAModificar.Description = dto.Description;
                contactoAModificar.Name = dto.Name;
                contactoAModificar.TelephoneNumber = dto.TelephoneNumber;
                //contactoAModificar.Groups = dto.Groups;

                _context.SaveChanges();
            }
        }

        public void Delete(int id, int userId)
        {
            _context.Contacts.Remove(_context.Contacts.Single(c => c.Id == id && c.UserId == userId));
            _context.SaveChanges();
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
