using LibraryAPI.Models;

namespace LibraryAPI.Data.Repositories

{

    public class LibraryItemRepository : ICrudRepository<LibraryItem, int> // T is ToDoItem and U is integer 
    {
        private readonly LibraryItemContext _libraryitemContext;
        public LibraryItemRepository(LibraryItemContext libraryitemContext)
        {
            _libraryitemContext = libraryitemContext ?? throw new
            ArgumentNullException(nameof(libraryitemContext));
        }
        public void Add(LibraryItem element)
        {
            _libraryitemContext.LibraryItems.Add(element); //adds a new object to that list
        }
        public void Delete(int id)
        {
            var item = Get(id);
            if (item is not null) _libraryitemContext.LibraryItems.Remove(Get(id));
        }
        public bool Exists(int id)
        {
            return _libraryitemContext.LibraryItems.Any(u => u.LibraryItemId == id);
        }
        public LibraryItem Get(int id)
        {
            return _libraryitemContext.LibraryItems.FirstOrDefault(u => u.LibraryItemId == id);
        }
        public IEnumerable<LibraryItem> GetAll()
        {
            return _libraryitemContext.LibraryItems.ToList();
        }
        public bool Save()
        {
            return _libraryitemContext.SaveChanges() > 0; //saves the changes and returns true if greater than 0 which means it was saved successfully
        }
        public void Update(LibraryItem element)
        {
            _libraryitemContext.Update(element);
        }
    }
}