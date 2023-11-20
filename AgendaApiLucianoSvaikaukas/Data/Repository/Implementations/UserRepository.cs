
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


        ////////// GET //////////

        public User? GetById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public List<User> GetListUser()
        {
            return _context.Users.ToList();
        }


        ////////// POST //////////

        public User? ValidateUser(AuthenticationRequestBody authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }


        ////////// PUT //////////

        public void UpdateUserData(User user)
        {
            var userItem = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (userItem != null)
            {
                userItem.Name = user.Name;
                userItem.Email = user.Email;
                userItem.LastName = user.LastName;

                _context.SaveChanges();
            }
        }


        ////////// DELETE //////////

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }


    }
}
