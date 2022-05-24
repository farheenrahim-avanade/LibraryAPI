using LibraryAPI.Models;

namespace LibraryAPI.Data.Repositories
{
    public class UserRepository : ICrudRepository<User, int> // T is ToDoItem and U is integer 
    {
        private readonly LibraryItemContext _libraryitemContext;
        public UserRepository(LibraryItemContext todoContext)
        {
            _libraryitemContext = todoContext ?? throw new
            ArgumentNullException(nameof(todoContext));
        }
        public void Add(User element)
        {
            _libraryitemContext.Users.Add(element); //adds a new object to that list
        }
        public void Delete(int id)
        {
            var item = Get(id);
            if (item is not null) _libraryitemContext.Users.Remove(Get(id));
        }
        public bool Exists(int id)
        {
            return _libraryitemContext.Users.Any(u => u.UserId == id);
        }
        public User Get(int id)
        {
            return _libraryitemContext.Users.FirstOrDefault(u => u.UserId == id);
        }
        public IEnumerable<User> GetAll()
        {
            return _libraryitemContext.Users.ToList();
        }

        public IEnumerable<string> GetJoinedData()
        {
            List<User> users = _libraryitemContext.Users.ToList();
            List<LibraryItem> libraryItems = _libraryitemContext.LibraryItems.ToList();

            var result = from user in users
                         join libraryItem in libraryItems
                         on user.LibraryItemId equals libraryItem.LibraryItemId
                         select $" User: {user.FirstName} {user.LastName} , Library Item: {libraryItem.ItemName}";

            return result;
        }
        public bool Save()
        {
            return _libraryitemContext.SaveChanges() > 0; //saves the changes and returns true if greater than 0 which means it was saved successfully
        }
        public void Update(User element)
        {
            _libraryitemContext.Update(element);
        }
    }
}
