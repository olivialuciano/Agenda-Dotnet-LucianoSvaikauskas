
using AutoMapper;
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Models;
using AgendaApiLucianoSvaikaukas.Entities;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private AgendaContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public User? GetById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public User? ValidateUser(AuthenticationRequestBody authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Create(UserForCreationDTO dto)
        {
            User usuarioACrear = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                LastName = dto.LastName,
                Password = dto.Password,
            };
            _context.Users.Add(usuarioACrear);
            _context.SaveChanges();

        }


        public void Update(UserForCreationDTO dto)
        {
            _context.Users.Update(_mapper.Map<User>(dto));
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
        }

        public void Create(UserForCreationDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
