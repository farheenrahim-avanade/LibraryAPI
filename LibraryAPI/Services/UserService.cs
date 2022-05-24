
using LibraryAPI.Data.Repositories;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class UserService : ICrudService<User, int>
    {
        private readonly ICrudRepository<User, int> _userRepository;
        public UserService(ICrudRepository<User, int> userRepository
            )
        {
            _userRepository = userRepository;
        }
        public void Add(User element) //dependency injection
        {
            _userRepository.Add(element); //added a new item to the list
            _userRepository.Save(); //after adding it, needs to be saved 
        }
        public void Delete(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
        }
        public User Get(int id)
        {
            return _userRepository.Get(id);
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public void Update(User old, User newT)
        {   
            old.FirstName = newT.FirstName;
            old.LastName = newT.LastName;
            _userRepository.Update(old);
            _userRepository.Save();
        }
    }
}
