using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;



namespace RepositoryLayer.Services
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressBookDbContext _context;

        public AddressBookRL(AddressBookDbContext context)
        {
            _context = context;
        }

        public async Task<List<AddressBookDTO>> GetAllContacts()
        {
            return await _context.AddressBookEntries.ToListAsync();
        }

        public async Task<AddressBookDTO> GetContactById(int id)
        {
            return await _context.AddressBookEntries.FindAsync(id);
        }

        public async Task<AddressBookDTO> AddContact(AddressBookDTO contact)
        {
            _context.AddressBookEntries.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<AddressBookDTO> UpdateContact(int id, AddressBookDTO contact)
        {
            var existingContact = await _context.AddressBookEntries.FindAsync(id);
            if (existingContact == null) return null;

            existingContact.Name = contact.Name;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Address = contact.Address;

            await _context.SaveChangesAsync();
            return existingContact;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = await _context.AddressBookEntries.FindAsync(id);
            if (contact == null) return false;

            _context.AddressBookEntries.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
