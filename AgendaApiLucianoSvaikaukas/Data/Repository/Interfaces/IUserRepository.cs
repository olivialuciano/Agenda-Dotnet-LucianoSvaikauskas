
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IUserRepository
    {

        ////////// GET //////////
        public User GetById(int userId);
        public List<User> GetAll();
        User GetUser(int id);
        List<User> GetListUser();

        ////////// POST //////////
        User AddUser(User user);
        public User ValidateUser(AuthenticationRequestBody authRequestBody);

        ////////// PUT //////////
        public void UpdateUserData(User user);

        ////////// DELETE //////////
        void DeleteUser(User user);
    }
}
