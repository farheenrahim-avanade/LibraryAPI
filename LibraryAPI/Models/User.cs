
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "Length of name cannot be less than 2 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Length of name cannot be less than 2 characters")]
        public string? LastName { get; set; }

        //This sets up a foreign key with the toDoItem to allow us to grab the item based on the person
        public int LibraryItemId { get; set; }
        //public ICollection<LibraryItem> LibraryItems { get; set; }

    }
}