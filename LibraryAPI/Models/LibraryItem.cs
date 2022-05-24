using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class LibraryItem
    {
        [Key]
        public int LibraryItemId { get; set; }

        [Required(ErrorMessage = "Name of the item is required")]
        [MaxLength(100, ErrorMessage = "Length of item name cannot be greater than 100 characters")]
        [MinLength(2, ErrorMessage = "Length of item cannot be less than 2 characters")]
        public string? ItemName { get; set; }

        [Required(ErrorMessage = "Item type is required")]
        [MaxLength(64, ErrorMessage = "Length of item type cannot be greater than 64 characters")]
        [MinLength(2, ErrorMessage = "Length of item type cannot be less than 2 characters")]
        public string? ItemType { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        //Ensures an employee id is required when creating a new to do item

        //public int? UserId { get; set; }

        //public Employee Employee { get; set; }
    }
}
