using LibraryAPI.Data.Repositories;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class LibraryItemService : ICrudService<LibraryItem, int>
    {
        private readonly ICrudRepository<LibraryItem, int> _libraryitemRepository;
        public LibraryItemService(ICrudRepository<LibraryItem, int> libraryitemRepository)
        {
            _libraryitemRepository = libraryitemRepository;
        }
        public void Add(LibraryItem element) //dependency injection
        {
            _libraryitemRepository.Add(element); //added a new item to the list
            _libraryitemRepository.Save(); //after adding it, needs to be saved 
        }
        public void Delete(int id)
        {
            _libraryitemRepository.Delete(id);
            _libraryitemRepository.Save();
        }
        public LibraryItem Get(int id)
        {
            return _libraryitemRepository.Get(id);
        }
        public IEnumerable<LibraryItem> GetAll()
        {
            return _libraryitemRepository.GetAll();
        }
        public void Update(LibraryItem old, LibraryItem newT)
        {
            old.ItemName = newT.ItemName;
            old.ItemType = newT.ItemType;
            old.Status = newT.Status;
            _libraryitemRepository.Update(old);
            _libraryitemRepository.Save();
        }
    }
}
