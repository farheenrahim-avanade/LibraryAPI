using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryAPI.Data
{
    public class LibraryItemContext : DbContext
    {
        public LibraryItemContext(DbContextOptions<LibraryItemContext> options) : base(options)
        {
        }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<User> Users { get; set; } //created data set for employees table
    }
}
