using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;

namespace RepositoryLayer.Context
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AddressBookEntry> AddressBookEntries { get; set; }
    }

}
