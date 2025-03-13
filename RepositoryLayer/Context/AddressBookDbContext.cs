using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;

namespace RepositoryLayer.Context
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options) { }

        public DbSet<AddressBookDTO> AddressBookEntries { get; set; }
    }
}
