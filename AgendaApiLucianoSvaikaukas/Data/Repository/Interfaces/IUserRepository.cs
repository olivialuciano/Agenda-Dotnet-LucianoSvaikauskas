
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;

namespace AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User ValidateUser(AuthenticationRequestBody authRequestBody);
        public User GetById(int userId);
        public List<User> GetAll();
        User GetUser(int id);
        public void UpdateUserData(User user);
        List<User> GetListUser();

        User AddUser(User user);
        void DeleteUser(User user);
    }
}
